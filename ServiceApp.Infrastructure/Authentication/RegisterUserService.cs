using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        if (!await ValidateFamilyAsync(familyId))
        {
            return new RegisterUserResponse
            {
                Succeeded = false,
                Errors = new List<string> { "Family does not exist." }
            };
        }

        var user = await CreateUserAsync(userName, email, password, familyId);
        if (user == null)
        {
            return new RegisterUserResponse
            {
                Succeeded = false,
                Errors = new List<string> { "Failed to create user." }
            };
        }

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
        if (!await ValidateFamilyAsync(familyId))
        {
            return new RegisterUserResponse
            {
                Succeeded = false,
                Errors = new List<string> { "Family does not exist." }
            };
        }

        var user = await CreateUserAsync(userName, email, password, familyId);
        if (user == null)
        {
            return new RegisterUserResponse
            {
                Succeeded = false,
                Errors = new List<string> { "Failed to create user." }
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

    public async Task<RegisterUserResponse> RegisterNewChildUserAsync(string userName, string email, string password, string familyId, List<string> childId)
    {
        if (!await ValidateFamilyAsync(familyId))
        {
            return new RegisterUserResponse
            {
                Succeeded = false,
                Errors = new List<string> { "Family does not exist." }
            };
        }

        var childUser = await CreateUserAsync(userName, email, password, familyId);
        if (childUser == null)
        {
            return new RegisterUserResponse
            {
                Succeeded = false,
                Errors = new List<string> { "Failed to create user." }
            };
        }

        await AddChildRoleAsync(childUser);
        await UpdateFamilyMembersAsync(childUser, familyId);
        await AssignParentRoleAsync(familyId);
        await _emailManagerService.SendConfirmationEmailAsync(childUser);

        return new RegisterUserResponse
        {
            Succeeded = true,
            Errors = new List<string>()
        };
    }

    private async Task<bool> ValidateFamilyAsync(string familyId)
    {
        bool isNewFamily = string.IsNullOrWhiteSpace(familyId);
        return isNewFamily || await _familyExistService.FamilyExists(familyId);
    }

    private async Task<User?> CreateUserAsync(string userName, string email, string password, string familyId)
    {
        var childUser = new User
        {
            UserName = userName,
            Email = email,
            FamilyId = string.IsNullOrWhiteSpace(familyId) ? Guid.NewGuid().ToString() : familyId
        };

        var result = await _userManager.CreateAsync(childUser, password);
        return result.Succeeded ? childUser : null;
    }

    private async Task UpdateFamilyMembersAsync(User childUser, string familyId)
    {
        var familyMembers = await _userManager.Users.Where(u => u.FamilyId == familyId).ToListAsync();
        foreach (var member in familyMembers)
        {
            var roles = await _userRolesService.GetRoleByUserId(member.Id);
            if (roles?.Value?.Contains("Child") == true)
            {
                member.ChildIds ??= new List<string>();
                member.ChildIds.Add(childUser.Id);
                await _userManager.UpdateAsync(member);

                if (member.Id != childUser.Id)
                {
                    childUser.ChildIds.Add(member.Id);
                }
            }
        }
    }

    private async Task AddChildRoleAsync(User childUser)
    {
        await _userRolesService.AddRoleToTheUser(childUser, "Child");
    }

    private async Task AssignParentRoleAsync(string familyId)
    {
        var familyUsers = await _userManager.Users.Where(u => u.FamilyId == familyId).ToListAsync();
        foreach (var user in familyUsers)
        {
            var userRoles = await _userRolesService.GetRoleByUserId(user.Id);
            if (userRoles?.Value != null &&
                (userRoles.Value.Contains("FamilyAdmin") || userRoles.Value.Contains("FamilyMember")))
            {
                if (!userRoles.Value.Contains("Parent"))
                {
                    await _userRolesService.AddRoleToTheUser(user, "Parent");
                }
            }
        }
    }
}

