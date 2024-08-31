namespace ServiceApp.Application.Authentication;
public interface IUserService
{
    Task<string> GetCurrentUserByIdAsync();
    Task<string> GetCurrentFamilyIdAsync();
    Task<string> GetUserId(string userId);
    Task<bool> IsCurrentUserInToleAsync(string role);

}
