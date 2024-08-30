using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceApp.Application.Authentication;
using ServiceApp.Application.Exceptions;

namespace ServiceApp.Infrastructure.Users;
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetCurrentFamilyIdAsync()
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
        {
            throw new UserNotAuthorizedException();
        }
        return user.FamilyId;
    }

    public async Task<string> GetCurrentUserByIdAsync()
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
        {
            throw new UserNotAuthorizedException();
        }
        return user.Id;
    }

    public async Task<string> GetUserId(string userId)
    {
        var user = await GetUserAsync(userId);
        return user?.Id ?? string.Empty;
    }

    public async Task<bool> IsCurrentUserInToleAsync(string role)
    {
        var user = await GetCurrentUserAsync();
        var result = user is not null && await _userManager.IsInRoleAsync(user, role);
        return result;
    }

    private async Task<User?> GetCurrentUserAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null || httpContext.User is null)
        {
            return null;
        }

        var user = await _userManager.GetUserAsync(httpContext.User);
        return user;
    }

    private async Task<User?> GetUserAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }
}
