using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace QuizApp.Models;

[Table("Users", Schema = "security")]
public class User : IdentityUser<Guid>, IMasterDataBaseEntity
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string DisplayName => $"{FirstName} {LastName}";

    public string? Avatar { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public Guid? CreatedById { get; set; }

    public User? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey(nameof(UpdatedBy))]
    public Guid? UpdatedById { get; set; }

    public User? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    [ForeignKey(nameof(DeletedBy))]
    public Guid? DeletedById { get; set; }

    public User? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsActive { get; set; }
}
