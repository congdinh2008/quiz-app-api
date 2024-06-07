using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;

namespace QuizApp.Business;

public class GetAllQuizCommandHandler : IRequestHandler<GetAllQuizQuery, IEnumerable<QuizViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllQuizCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<QuizViewModel>> Handle(GetAllQuizQuery request, CancellationToken cancellationToken)
    {
        if (request.IncludeDeleted)
        {
            var query = _unitOfWork.QuizRepository.GetQueryWithInactive(x => x.DeletedAt != null || x.DeletedAt == null);
            return await query.Select(q => new QuizViewModel
            {
                Id = q.Id,
                Title = q.Title,
                Description = q.Description,
                Duration = q.Duration,
                ThubmnailUrl = q.ThubmnailUrl
            }).ToListAsync();
        }
        else
        {
            var query = _unitOfWork.QuizRepository.GetQuery(x=> x.IsActive == true || x.IsActive == false);
            return await query.Select(q => new QuizViewModel
            {
                Id = q.Id,
                Title = q.Title,
                Description = q.Description,
                Duration = q.Duration,
                ThubmnailUrl = q.ThubmnailUrl
            }).ToListAsync();
        }
    }
}