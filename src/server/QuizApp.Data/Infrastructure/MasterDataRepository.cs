using QuizApp.Core;
using QuizApp.Models;

namespace QuizApp.Data;

public class MasterDataRepository<T> : MasterDataRepositoryBase<T, QuizAppDbContext> where T : class,
        IMasterDataBaseEntity
{
    private readonly IUserIdentity _currentUser;

    public MasterDataRepository(QuizAppDbContext dataContext, IUserIdentity currentUser)
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