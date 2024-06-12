using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Core;
using QuizApp.Data;

namespace QuizApp.Business;

public class QuizSearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<QuizSearchQuery, PaginatedResult<QuizViewModel>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedResult<QuizViewModel>> Handle(QuizSearchQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.QuizRepository.GetQuery();

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            query = query.Where(x => x.Title.Contains(request.Keyword) || (x.Description != null && x.Description.Contains(request.Keyword)));
        }

        var totalItems = await query.CountAsync();

        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query.OrderByExtension(request.OrderBy, request.OrderDirection.ToString());
        }
        else
        {
            query = query.OrderBy(x => x.Title);
        }

        var items = await query
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync();

        return new PaginatedResult<QuizViewModel>(
            request.PageNumber,
            request.PageSize,
            totalItems,
            _mapper.Map<IEnumerable<QuizViewModel>>(items)
        );
    }
}
