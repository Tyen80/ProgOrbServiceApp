using ServiceApp.Domain.Users;

namespace ServiceApp.Application.Authentication;
public interface IEmailManagerService
{
    Task SendConfirmationEmailAsync(IUser user);
}
