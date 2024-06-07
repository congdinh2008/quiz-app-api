using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Data;

public class QuizAppDbContext : IdentityDbContext<User, Role, Guid>
{
    IHttpContextAccessor _httpContextAccessor;
    public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Quiz> Quizzes { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public DbSet<QuizQuestion> QuizQuestions { get; set; }

    public DbSet<UserQuiz> UserQuizzes { get; set; }

    public DbSet<UserAnswer> UserAnswers { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users", "security");
        builder.Entity<Role>().ToTable("Roles", "security");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "security");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "security");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "security");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "security");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "security");


        // Config many to many between Quiz-Question
        builder.Entity<QuizQuestion>().HasKey(x => new { x.QuizId, x.QuestionId });

        builder.Entity<UserAnswer>().HasKey(x => new { x.UserId, x.QuestionId, x.UserQuizId });

        builder.Entity<UserAnswer>().HasOne(x => x.Question).WithMany().HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserAnswer>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

    }

    public override int SaveChanges()
    {
        BeforeSaveChange();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        BeforeSaveChange();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void BeforeSaveChange()
    {
        // Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid currentUserId);

        var entities = this.ChangeTracker.Entries<IBaseEntity>();

        foreach (var item in entities)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    item.Entity.CreatedAt = DateTime.Now;
                    item.Entity.CreatedById = null;
                    break;
                case EntityState.Modified:
                    item.Entity.UpdatedAt = DateTime.Now;
                    item.Entity.UpdatedById = null;
                    break;
            }
        }
    }
}
