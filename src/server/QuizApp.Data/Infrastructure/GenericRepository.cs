using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Data;

public class GenericRepository<T, TContext> : IGenericRepository<T> where T : class where TContext : QuizAppDbContext
{
    #region Protected Fields

    protected readonly TContext DataContext;
    protected readonly DbSet<T> DbSet;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryBase{T, TDbContext}"/> class.
    /// </summary>
    /// <param name="dataContext">The data context.</param>
    public GenericRepository(TContext dataContext)
    {
        this.DataContext = dataContext;

        // Find Property with typeof(T) on dataContext
        var typeOfDbSet = typeof(DbSet<T>);
        foreach (var prop in dataContext.GetType().GetProperties())
        {
            if (typeOfDbSet == prop.PropertyType)
            {
                if (prop.GetValue(dataContext, null) is DbSet<T> dbSet)
                {
                    DbSet = dbSet;
                }
                break;
            }
        }
        DbSet ??= dataContext.Set<T>();
    }

    #region Virtual Methods

    public virtual void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Add(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Add(entity);
        }
    }

    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
    }

    /// <summary>
    /// Deletes the specified context.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="isHardDelete">is hard delete?</param>
    public virtual void Delete(T entity, bool isHardDelete = false)
    {
        DbSet.Remove(entity);
    }

    /// <summary>
    /// Deletes the specified context.
    /// </summary>
    /// <param name="entities">List of entity.</param>
    /// <param name="isHardDelete">is hard delete?</param>
    public virtual void Delete(IEnumerable<T> entities, bool isHardDelete = false)
    {
        foreach (var entity in entities)
        {
            Delete(entity, isHardDelete);
        }
    }

    /// <summary>
    /// Deletes the specified context.
    /// </summary>
    /// <param name="where">The where.</param>
    /// <param name="isHardDelete">is hard delete?</param>
    public virtual void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false)
    {
        var entities = GetQuery(where).AsEnumerable();
        foreach (var entity in entities)
        {
            Delete(entity, isHardDelete);
        }
    }

    /// <summary>
    /// Gets the by identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public virtual T? GetById(Guid id)
    {
        return DbSet.Find(id);
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual IQueryable<T> GetQuery()
    {
        return DbSet.AsQueryable();
    }

    public virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> where)
    {
        return GetQuery().Where(where);
    }

    #endregion
}
