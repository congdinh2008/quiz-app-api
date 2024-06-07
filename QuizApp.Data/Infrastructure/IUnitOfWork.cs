using QuizApp.Models;

namespace QuizApp.Data;

public interface IUnitOfWork : IDisposable
{
    QuizAppDbContext Context { get; }

    #region Master Data Repositories
    IMasterDataRepository<User> UserRepository { get; }

    IMasterDataRepository<Role> RoleRepository { get; }

    IMasterDataRepository<Quiz> QuizRepository { get; }

    IMasterDataRepository<Question> QuestionRepository { get; }

    IMasterDataRepository<Answer> AnswerRepository { get; }

    #endregion

    #region Repositories
    IGenericRepository<QuizQuestion> QuizQuestionRepository { get; }

    IGenericRepository<UserQuiz> UserQuizzesRepository { get; }

    IRepository<T> Repository<T>() where T : BaseEntity, IBaseEntity;

    #endregion

    int SaveChanges();

    Task<int> SaveChangesAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}