using System.ComponentModel.DataAnnotations;
using MediatR;

namespace QuizApp.Business;

public class UserLoginQuery: IRequest<LoginResultViewModel>
{
    [Required(ErrorMessage = "{0} is required")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public required string Password { get; set; }
}
