namespace QuizApp.Business;

public class UserInformationViewModel
{
    public required string Id { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public required string UserName { get; set; }

    public required string DisplayName { get; set; }

    public required string Avatar { get; set; }

    public required string Address { get; set; }

    public required bool IsActive { get; set; }

    public ICollection<string> Roles { get; set; } = [];
}