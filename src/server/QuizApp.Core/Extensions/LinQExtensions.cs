namespace QuizApp.Core;

using System.Linq.Expressions;

public static class LinQExtensions
    {
        public static Expression<Func<T, TKey>> OrderExpression<T, TKey>(string memberName)
        {
            ParameterExpression[] typeParams = [Expression.Parameter(typeof(T), "")];

            Expression<Func<T, TKey>> orderByExpression = (Expression<Func<T, TKey>>)Expression.Lambda(
                    Expression.Property(typeParams[0], memberName), typeParams);
            return orderByExpression;
        }

        public static IQueryable<T> OrderByExtension<T>(this IQueryable<T> input, string memberName, string sord)
        {
            string methodName = $"OrderBy{(sord.ToLower() == "asc" ? "" : "descending")}";

            ParameterExpression parameter = Expression.Parameter(input.ElementType, "p");

            MemberExpression? memberAccess = null;
            foreach (var property in memberName.Split('.'))
                memberAccess = Expression.Property
                    (memberAccess ?? (Expression)parameter, property);

            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);

            MethodCallExpression result = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { input.ElementType, memberAccess.Type },
                input.Expression,
                Expression.Quote(orderByLambda));

            return input.Provider.CreateQuery<T>(result);
        }

        public static IEnumerable<T> OrderByExtension<T>(this IEnumerable<T> source, string sortBy, string sortDirection)
        {
            var param = Expression.Parameter(typeof(T), "item");

            var sortExpression = Expression.Lambda<Func<T, object>>
                (Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

            switch (sortDirection.ToLower())
            {
                case "asc":
                    return source.AsQueryable<T>().OrderBy<T, object>(sortExpression);
                default:
                    return source.AsQueryable<T>().OrderByDescending<T, object>(sortExpression);
            }
        }

        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        /// <summary>
        /// Get Name of Property of Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string? GetPropertyName<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            if (expression.Body is MemberExpression me)
            {
                return me.Member.Name;
            }
            return null;
        }

        public static Func<T, object>? GetOrderByExpression<T>(string sortColumn)
        {
            Func<T, object>? orderByExpr = null;
            if (!string.IsNullOrEmpty(sortColumn))
            {
                Type sponsorResultType = typeof(T);

                if (sponsorResultType.GetProperties().Any(prop => prop.Name == sortColumn))
                {
                    System.Reflection.PropertyInfo pinfo = sponsorResultType.GetProperty(sortColumn);
                    orderByExpr = data => pinfo.GetValue(data, null);
                }
            }
            return orderByExpr;
        }

        public static Expression<Func<T, object>> OrderExpression<T>(string memberName)
        {
            var func = GetOrderByExpression<T>(memberName);

            var orderByExpression = Expression.Lambda<Func<T, object>>(Expression.Call(func.Method));

            return orderByExpression;
        }

        public static Func<T, bool> AndAlso<T>(
            this Func<T, bool> predicate1,
            Func<T, bool> predicate2)
        {
            return arg => predicate1(arg) && predicate2(arg);
        }

        public static Func<T, bool> OrElse<T>(
            this Func<T, bool> predicate1,
            Func<T, bool> predicate2)
        {
            return arg => predicate1(arg) || predicate2(arg);
        }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {

            // build parameter map (from parameters of second to parameters of first)

            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);



            // replace parameters in the second lambda expression with parameters from the first

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);



            // apply composition of lambda expression bodies to parameters from the first expression 

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);

        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (map.TryGetValue(node, out var replacement))
            {
                node = replacement;
            }
            return base.VisitParameter(node);
        }
    }