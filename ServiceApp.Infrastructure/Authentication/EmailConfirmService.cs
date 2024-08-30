using Microsoft.AspNetCore.Identity;
using ServiceApp.Application.Authentication;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.Infrastructure.Authentication;
public class EmailConfirmService : IEmailConfirmService
{
    private readonly UserManager<User> _userManager;

    public EmailConfirmService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded;
    }
}
