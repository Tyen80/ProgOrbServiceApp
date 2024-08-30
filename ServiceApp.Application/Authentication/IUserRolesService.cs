using ServiceApp.Domain.Users;

namespace ServiceApp.Application.Authentication;
public interface IUserRolesService
{
    Task<Result> AddRoleToTheUser(IUser user, string roleName);
    Task<Result<List<string>>> GetRoleByUserId(string userId);
    Task<Result<List<string>>> GetAllRoles(IUser user);
}
