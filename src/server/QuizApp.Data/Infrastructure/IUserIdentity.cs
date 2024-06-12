namespace QuizApp.Data;

public interface IUserIdentity
{
    Guid UserId { get; }

    string UserName { get; }
}
