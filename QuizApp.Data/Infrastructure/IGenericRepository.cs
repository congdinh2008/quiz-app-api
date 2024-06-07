using System.Linq.Expressions;

namespace QuizApp.Data;

public interface IGenericRepository<T> where T : class
{
    void Add(T entity);

    void Add(IEnumerable<T> entities);

    void Update(T entity);

    void Delete(T entity, bool isHardDelete = false);

    void Delete(IEnumerable<T> entities, bool isHardDelete = false);

    void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false);

    IQueryable<T> GetQuery();

    IQueryable<T> GetQuery(Expression<Func<T, bool>> where);
}
