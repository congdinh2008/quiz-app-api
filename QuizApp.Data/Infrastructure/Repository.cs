using QuizApp.Core;
using QuizApp.Models;

namespace QuizApp.Data;

public class Repository<T> : RepositoryBase<T, QuizAppDbContext> where T : class, IBaseEntity
{
    private readonly IUserIdentity _currentUser;

    public Repository(QuizAppDbContext dataContext, IUserIdentity currentUser)
        : base(dataContext)
    {
        _currentUser = currentUser;
    }

    protected override Guid CurrentUserId
    {
        get
        {
            if (_currentUser != null)
            {
                return _currentUser.UserId;
            }

            return Constants.SystemAdministratorId;
        }
    }

    protected override string CurrentUserName
    {
        get
        {
            if (_currentUser != null)
            {
                return _currentUser.UserName;
            }

            return Constants.UserName.SystemAdministrator;
        }
    }
}
