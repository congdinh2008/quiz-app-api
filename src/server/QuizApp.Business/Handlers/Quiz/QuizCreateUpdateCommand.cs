using System.ComponentModel.DataAnnotations;

namespace QuizApp.Business;

public class QuizCreateUpdateCommand : BaseCreateCommand<bool>
{
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
