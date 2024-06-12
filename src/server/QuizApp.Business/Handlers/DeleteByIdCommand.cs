using MediatR;

namespace QuizApp.Business;

public class DeleteByIdCommand<T> : IRequest<T>
{
    public Guid Id { get; set; }

    public bool IsHardDelete { get; set; } = false;
}
