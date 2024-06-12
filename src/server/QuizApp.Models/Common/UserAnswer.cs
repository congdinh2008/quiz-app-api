using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;

[Table("UserAnswers", Schema = "common")]
public class UserAnswer
{
    [ForeignKey(nameof(UserQuiz))]
    public Guid UserQuizId { get; set; }

    public required UserQuiz UserQuiz { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public required User User { get; set; }



    [ForeignKey(nameof(Question))]
    public Guid QuestionId { get; set; }

    public required Question Question { get; set; }

    [ForeignKey(nameof(Answer))]
    public Guid AnswerId { get; set; }

    public required Answer Answer { get; set; }
}
