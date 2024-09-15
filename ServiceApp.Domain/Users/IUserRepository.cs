namespace ServiceApp.Domain.Users;
public interface IUserRepository
{
    Task<List<IUser>> GetFamilyMembersAsync(string familyId);
    Task<List<IUser>> GetFamilyMembersByRoleAsync(string familyId);
    Task<List<IUser>> GetAllUsersByFamilyIdWithRolesAsync();
}
