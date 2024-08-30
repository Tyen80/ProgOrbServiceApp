namespace ServiceApp.Application.Authentication;

public interface ILoginUserService
{
    Task<bool> LoginAsync(string userName, string password);
}
