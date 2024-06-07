using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;

[Table("UserQuizzes", Schema ="common")]
public class UserQuiz
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(Quiz))]
    public Guid QuizId { get; set; }

    public required Quiz Quiz { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public required User User { get; set; }

    public Guid QuizCode { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ICollection<UserAnswer> UserAnswers { get; set; } = [];
}
