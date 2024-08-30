namespace ServiceApp.Application.Authentication;
public interface IUserService
{
    Task<string> GetCurrentUserByIdAsync();
    Task<string> GetCurrentFamilyIdAsync();
    Task<bool> IsCurrentUserInToleAsync(string role);
}
