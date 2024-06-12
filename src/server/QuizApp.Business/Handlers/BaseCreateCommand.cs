using MediatR;

namespace QuizApp.Business;

public class BaseCreateCommand<T>: IRequest<T>
{
    public Guid? Id { get; set; }
}
