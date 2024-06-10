using MediatR;
using QuizApp.Core;

namespace QuizApp.Business;

public class QuizSearchQuery : SearchQuery, IRequest<PaginatedResult<QuizViewModel>>
{
}

