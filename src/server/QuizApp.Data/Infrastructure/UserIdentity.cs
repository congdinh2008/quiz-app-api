using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizApp.Models;

namespace QuizApp.Data;

public class UserIdentity : IUserIdentity
{
    private readonly UserManager<User> _userManager;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserIdentity(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public virtual Guid UserId => GetCurrentUserAsync().Result?.Id ?? Guid.Empty;

    private async Task<User?> GetCurrentUserAsync()
    {
        var email = _httpContextAccessor.HttpContext?.User?.Identity?.Name?.ToUpper();

        if (email == null)
        {
            return null;
        }

        var currentUser = await _userManager.FindByNameAsync(email);

        return currentUser;
    }

    public virtual string UserName => GetCurrentUserAsync().Result?.UserName ?? string.Empty;
}
