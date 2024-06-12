using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;

[Table("Quizzes", Schema = "common")]
public class Quiz : MasterDataBaseEntity, IMasterDataBaseEntity
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required int Duration { get; set; }

    public string? ThubmnailUrl { get; set; }        

    public ICollection<QuizQuestion> QuizQuestions { get; set; } = [];

    public ICollection<UserQuiz> UserQuizzes { get; set; } = [];
}
