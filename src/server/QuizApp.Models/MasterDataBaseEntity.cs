namespace QuizApp.Models;

// For Manager

public class MasterDataBaseEntity : BaseEntity, IMasterDataBaseEntity
{
    public bool IsActive { get; set; }
}
