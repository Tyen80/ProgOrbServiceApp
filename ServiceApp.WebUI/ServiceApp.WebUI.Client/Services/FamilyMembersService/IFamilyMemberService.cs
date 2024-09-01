using ServiceApp.Application.Users;

namespace ServiceApp.WebUI.Client.Services.FamilyMembersService;

public interface IFamilyMemberService
{
    Task<List<UsersDto>> GetAllUsersByFamilyId();
    Task<List<UsersDto>> GetFamilyMembersByRoleAsync();
}
