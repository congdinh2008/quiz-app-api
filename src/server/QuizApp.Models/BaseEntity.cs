using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }

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

    public bool IsDeleted { get; set;}
}
