using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Business;

public class CreateUpdateQuizCommand : IRequest<bool>
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(100, ErrorMessage = "{0} must be at least {2} and at max {1} characters long", MinimumLength = 3)]
    public required string Title { get; set; }

    [MaxLength(500, ErrorMessage = "{0} must be at max {1} characters long")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Range(1, 3600, ErrorMessage = "{0} must be between {1} and {2}")]
    public int Duration { get; set; }

    [MaxLength(500, ErrorMessage = "{0} must be at max {1} characters long")]
    public string? ThubmnailUrl { get; set; }
}


public class CreateUpdateQuizCommandHandler : IRequestHandler<CreateUpdateQuizCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUpdateQuizCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateUpdateQuizCommand request, CancellationToken cancellationToken)
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

    private async Task<bool> CreateQuiz(CreateUpdateQuizCommand request)
    {
        var entity = await _unitOfWork.QuizRepository.GetQuery().FirstOrDefaultAsync(x => x.Title == request.Title);
        if (entity != null)
        {
            throw new Exception("Quiz with the same title already exists");
        }

        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Duration = request.Duration,
            ThubmnailUrl = request.ThubmnailUrl
        };

        _unitOfWork.QuizRepository.Add(quiz);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    private async Task<bool> UpdateQuiz(CreateUpdateQuizCommand request)
    {
        var entity = await _unitOfWork.QuizRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity == null)
        {
            throw new Exception("Quiz not exists");
        }

        entity.Id = Guid.NewGuid();
        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Duration = request.Duration;
        entity.ThubmnailUrl = request.ThubmnailUrl;

        _unitOfWork.QuizRepository.Update(entity);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}