using QuizApp.Models;

namespace QuizApp.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly QuizAppDbContext _context;

    private readonly IUserIdentity _currentUser;
    public UnitOfWork(QuizAppDbContext context, IUserIdentity currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    #region Implementation of Master Data Repositories
    private IMasterDataRepository<User>? _userRepository;
    public IMasterDataRepository<User> UserRepository => _userRepository ??= new MasterDataRepository<User>(_context, _currentUser);

    private IMasterDataRepository<Role>? _roleRepository;
    public IMasterDataRepository<Role> RoleRepository => _roleRepository ??= new MasterDataRepository<Role>(_context, _currentUser);

    private IMasterDataRepository<Quiz>? _quizRepository;
    public IMasterDataRepository<Quiz> QuizRepository => _quizRepository ??= new MasterDataRepository<Quiz>(_context, _currentUser);

    private IMasterDataRepository<Question>? _questionRepository;
    public IMasterDataRepository<Question> QuestionRepository => _questionRepository ??= new MasterDataRepository<Question>(_context, _currentUser);

    private IMasterDataRepository<Answer>? _answerRepository;
    public IMasterDataRepository<Answer> AnswerRepository => _answerRepository ??= new MasterDataRepository<Answer>(_context, _currentUser);

    #endregion

    #region Implementation of Repositories
    private IGenericRepository<QuizQuestion>? _quizQuestionRepository;
    public IGenericRepository<QuizQuestion> QuizQuestionRepository => _quizQuestionRepository ?? new GenericRepository<QuizQuestion, QuizAppDbContext>(_context);

    private IGenericRepository<UserQuiz>? _userQuizzesRepository;
    public IGenericRepository<UserQuiz> UserQuizzesRepository => _userQuizzesRepository ??= new GenericRepository<UserQuiz, QuizAppDbContext>(_context);

    public QuizAppDbContext Context => throw new NotImplementedException();

    public IRepository<T> Repository<T>() where T : BaseEntity, IBaseEntity
    {
        return new Repository<T>(_context, _currentUser);
    }

    #endregion

    public void Dispose()
    {
        this._context.Dispose();
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
}
