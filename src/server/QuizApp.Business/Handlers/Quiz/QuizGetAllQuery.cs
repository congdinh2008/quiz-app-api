using MediatR;

namespace QuizApp.Business;

public class QuizGetAllQuery : IRequest<IEnumerable<QuizViewModel>>
{
    public bool IncludeDeleted { get; set; }
}

