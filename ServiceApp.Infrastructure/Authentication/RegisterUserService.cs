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

    public async Task<RegisterUserResponse> RegisterUserAsync(string userName, string email, string password, string familyId)
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

        // Determine the role based on whether the family exists or if it's a new family
        string role = isNewFamily || !familyExists ? "FamilyAdmin" : "FamilyMember";
        await _userRolesService.AddRoleToTheUser(user, role); // Adjusted to pass the role directly

        await _emailManagerService.SendConfirmationEmailAsync(user);

        return new RegisterUserResponse
        {
            Succeeded = true,
            Errors = new List<string>()
        };
    }
}
