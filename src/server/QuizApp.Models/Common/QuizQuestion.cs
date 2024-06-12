using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;

[Table("QuizQuestions", Schema ="common")]
public class QuizQuestion
{
    [ForeignKey(nameof(Quiz))]
    public Guid QuizId { get; set; }

    public required Quiz Quiz { get; set; }

    [ForeignKey(nameof(Question))]
    public Guid QuestionId { get; set; }

    public required Question Question { get; set; }

    public int Order { get; set; }
}
