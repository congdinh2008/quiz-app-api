using AutoMapper;
using MediatR;
using QuizApp.Data;

namespace QuizApp.Business;

public class QuizGetByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<QuizGetByIdQuery, QuizViewModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<QuizViewModel> Handle(QuizGetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.QuizRepository.GetByIdAsync(request.Id) ??
        throw new KeyNotFoundException(string.Format("Quiz with id {0} not found", request.Id));

        return _mapper.Map<QuizViewModel>(entity);
    }
}

public class QuizDeleteByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<QuizDeleteByIdCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> Handle(QuizDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.QuizRepository.GetByIdAsync(request.Id) ??
        throw new KeyNotFoundException(string.Format("Quiz with id {0} not found", request.Id));

        _unitOfWork.QuizRepository.Delete(entity, request.IsHardDelete);

        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}