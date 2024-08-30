using Microsoft.AspNetCore.Identity;
using ServiceApp.Application.Authentication;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.Infrastructure.Authentication;
public class LoginUserService : ILoginUserService
{
    private readonly SignInManager<User> _signInManager;

    public LoginUserService(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<bool> LoginAsync(string userName, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
        return result.Succeeded;
    }
}
