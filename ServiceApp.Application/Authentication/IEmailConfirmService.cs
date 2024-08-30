namespace ServiceApp.Application.Authentication;
public interface IEmailConfirmService
{
    Task<bool> ConfirmEmail(string userId, string token);
}
