using Microsoft.AspNetCore.Identity;
using ServiceApp.Application.Authentication;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.Infrastructure.Authentication;
public class RegisterUserService : IRegisterUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IFamilyExistService _familyExistService;
    private readonly IEmailManagerService _emailManagerService;
    private readonly IUserRolesService _userRolesService;

    public RegisterUserService(UserManager<User> userManager, IFamilyExistService familyExistService, IEmailManagerService emailManagerService, IUserRolesService userRolesService)
    {
        _userManager = userManager;
        _familyExistService = familyExistService;
        _emailManagerService = emailManagerService;
        _userRolesService = userRolesService;
    }

    public async Task<RegisterUserResponse> RegisterNewUserAsync(string userName, string email, string password, string familyId)
    {
        bool isNewFamily = string.IsNullOrWhiteSpace(familyId);
        bool familyExists = !isNewFamily && await _familyExistService.FamilyExists(familyId);

        var user = new User
        {
            UserName = userName,
            Email = email,
            FamilyId = isNewFamily ? Guid.NewGuid().ToString() : familyId
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return new RegisterUserResponse
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        // Use the provided role directly
        await _userRolesService.AddRoleToTheUser(user, "FamilyAdmin");

        await _emailManagerService.SendConfirmationEmailAsync(user);

        return new RegisterUserResponse
        {
            Succeeded = true,
            Errors = new List<string>()
        };
    }

    public async Task<RegisterUserResponse> RegisterInvitedUserAsync(string userName, string email, string password, string familyId, string role)
    {
        bool isNewFamily = string.IsNullOrWhiteSpace(familyId);
        bool familyExists = !isNewFamily && await _familyExistService.FamilyExists(familyId);

        var user = new User
        {
            UserName = userName,
            Email = email,
            FamilyId = isNewFamily ? Guid.NewGuid().ToString() : familyId
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return new RegisterUserResponse
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        await _userRolesService.AddRoleToTheUser(user, role);

        await _emailManagerService.SendConfirmationEmailAsync(user);

        return new RegisterUserResponse
        {
            Succeeded = true,
            Errors = new List<string>()
        };
    }
}

