using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Business;

public class QuizCreateUpdateCommandHandler : IRequestHandler<QuizCreateUpdateCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public QuizCreateUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(QuizCreateUpdateCommand request, CancellationToken cancellationToken)
    {
        if (request.Id.HasValue)
        {
            return await UpdateQuiz(request);
        }
        else
        {
            return await CreateQuiz(request);
        }
    }

    private async Task<bool> CreateQuiz(QuizCreateUpdateCommand request)
    {
        var entity = await _unitOfWork.QuizRepository.GetQuery().FirstOrDefaultAsync(x => x.Title == request.Title);
        if (entity != null)
        {
            throw new Exception("Quiz with the same title already exists");
        }

        request.Id = Guid.NewGuid();

        var quiz = _mapper.Map<Quiz>(request);
        quiz.IsActive = true;

        _unitOfWork.QuizRepository.Add(quiz);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    private async Task<bool> UpdateQuiz(QuizCreateUpdateCommand request)
    {
        var entity = await _unitOfWork.QuizRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity == null)
        {
            throw new Exception("Quiz not exists");
        }

        _mapper.Map(request, entity);

        _unitOfWork.QuizRepository.Update(entity);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}