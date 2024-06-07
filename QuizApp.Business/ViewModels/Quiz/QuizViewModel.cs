namespace QuizApp.Business;

public class QuizViewModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required int Duration { get; set; }

    public string? ThubmnailUrl { get; set; }
}
