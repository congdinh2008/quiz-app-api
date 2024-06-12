using MediatR;

namespace QuizApp.Business;

public class GetByIdQuery<T>: IRequest<T>
{
    public Guid Id { get; set; }
}
