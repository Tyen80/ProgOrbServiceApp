namespace ServiceApp.Domain.Users;
public interface IUserRepository
{
    Task<List<IUser>> GetFamilyMembersAsync(string familyId);
}
