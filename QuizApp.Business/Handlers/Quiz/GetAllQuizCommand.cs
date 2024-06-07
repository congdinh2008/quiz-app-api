using MediatR;

namespace QuizApp.Business;

public class GetAllQuizQuery : IRequest<IEnumerable<QuizViewModel>>
{
    public bool IncludeDeleted { get; set; }
}
