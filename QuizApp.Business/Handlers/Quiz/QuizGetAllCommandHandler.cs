using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;

namespace QuizApp.Business;

public class QuizGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<QuizGetAllQuery, IEnumerable<QuizViewModel>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<QuizViewModel>> Handle(QuizGetAllQuery request, CancellationToken cancellationToken)
    {
        if (request.IncludeDeleted)
        {
            var entities = await _unitOfWork.QuizRepository.GetQueryWithDeleted().ToListAsync();
            return _mapper.Map<IEnumerable<QuizViewModel>>(entities);
        }
        else
        {
            var entities = await _unitOfWork.QuizRepository.GetQuery().ToListAsync();
            return _mapper.Map<IEnumerable<QuizViewModel>>(entities);
        }
    }
}
