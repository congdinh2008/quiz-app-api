using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;

[Table("Answers", Schema ="common")]
public class Answer : MasterDataBaseEntity, IMasterDataBaseEntity
{
    public required string Content { get; set; }

    public bool IsCorrect { get; set; }

    public required int Duration { get; set; }

    public Guid QuestionId { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public virtual Question? Question { get; set; }
}
