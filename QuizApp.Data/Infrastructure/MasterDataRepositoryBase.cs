using System.Linq.Expressions;
using QuizApp.Models;

namespace QuizApp.Data;

public abstract class MasterDataRepositoryBase<T, TContext> : RepositoryBase<T, TContext>, IMasterDataRepository<T>
        where T : class, IMasterDataBaseEntity
        where TContext : QuizAppDbContext
{

    #region Constructor
    protected MasterDataRepositoryBase(TContext dataContext) : base(dataContext)
    {
    }
    #endregion

    public virtual IEnumerable<T> GetAllWithInactive()
    {
        return GetQuery(true).ToList();
    }

    public virtual IEnumerable<T> GetManyWithInactive(Expression<Func<T, bool>> where)
    {
        return GetQuery(true).Where(where).ToList();
    }

    public IQueryable<T> GetQuery(bool includeInactive = false)
    {
        var query = base.GetQuery();
        if (includeInactive)
        {
            return query;
        }

        return query.Where(m => m.IsActive);
    }

    public new IQueryable<T> GetQuery(Expression<Func<T, bool>> where)
    {
        return GetQuery().Where(where);
    }

    public IQueryable<T> GetQueryWithInactive(Expression<Func<T, bool>> where)
    {
        return GetQuery(true).Where(where);
    }

    public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
    {
        return GetQuery().Where(where).ToList();
    }
}
