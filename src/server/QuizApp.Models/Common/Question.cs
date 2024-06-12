using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;

[Table("Questions", Schema ="common")]
public class Question : MasterDataBaseEntity, IMasterDataBaseEntity
{
    public required string Content { get; set; }

    public string? Description { get; set; }

    public required QuestionType QuestionType { get; set; }

    public ICollection<Answer> Answers { get; set; } = [];

    public ICollection<QuizQuestion> QuizQuestions { get; set; } = [];
}
