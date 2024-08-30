using Microsoft.AspNetCore.Identity;
using ServiceApp.Application.Authentication;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.Infrastructure.Authentication;
public class RegisterUserService : IRegisterUserService
{
    private readonly UserManager<User> _userManager;

    public RegisterUserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterUserResponse> RegisterUserAsync(string userName, string password, string familyId)
    {
        var user = new User
        {
            UserName = userName,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, password);

        await _userManager.AddToRoleAsync(user, "Parent");

        var response = new RegisterUserResponse
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
        return response;
    }
}
