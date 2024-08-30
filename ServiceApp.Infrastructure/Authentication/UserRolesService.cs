using Microsoft.AspNetCore.Identity;
using ServiceApp.Application.Authentication;
using ServiceApp.Domain.Abstractions;
using ServiceApp.Domain.Users;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.Infrastructure.Authentication;
public class UserRolesService : IUserRolesService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRolesService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result> AddRoleToTheUser(IUser user, string roleName)
    {


        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var userEntity = user as User;
        if (userEntity == null)
        {
            return Result.Fail("User not found");
        }

        await _userManager.AddToRoleAsync(userEntity, roleName);

        return Result.Ok();
    }

    public async Task<Result<List<string>>> GetAllRoles(IUser user)
    {
        var userEntity = user as User;
        if (userEntity == null)
        {
            return Result.Fail<List<string>>("User not found");
        }

        var roles = await _userManager.GetRolesAsync(userEntity);
        if (roles != null && roles.Count > 0)
        {
            return Result.Ok(roles.ToList());
        }

        return Result.Fail<List<string>>("No roles found");
    }

    public async Task<Result<List<string>>> GetRoleByUserId(string userId)
    {
        var userTest = await _userManager.FindByIdAsync(userId);
        if (userTest == null)
        {
            return Result.Fail<List<string>>("User not found");
        }
        var userRoles = await _userManager.GetRolesAsync(userTest) as List<string>;
        if ((userRoles != null))
        {
            return Result.Ok(userRoles);
        }
        return Result.Fail<List<string>>("User not found");
    }
}
