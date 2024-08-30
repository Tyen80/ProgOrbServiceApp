using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using ServiceApp.Application.Authentication;
using ServiceApp.Domain.Email;
using ServiceApp.Domain.Users;
using ServiceApp.Infrastructure.Users;
using System.Text;

namespace ServiceApp.Infrastructure.Authentication;
public class EmailManagerService : IEmailManagerService
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public EmailManagerService(UserManager<User> userManager, IEmailService emailService, IConfiguration configuration)
    {
        _userManager = userManager;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task SendConfirmationEmailAsync(IUser user)
    {
        var appUser = user as User;
        if (appUser == null)
        {
            throw new ArgumentException("User must be of type User", nameof(user));
        }

        if (string.IsNullOrWhiteSpace(appUser.Email))
        {
            throw new ArgumentException("User must have an email", nameof(user));
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var baseUrl = _configuration["ApplicationSettings:BaseUrl"];
        var callbackUrl = $"{baseUrl}/Account/ConfirmEmail?userId={appUser.Id}&code={encodedToken}";

        await _emailService.SendEmailAsync(appUser.Email, "Confirm your email", $"Please confirm your email by clicking <a href='{callbackUrl}'>here</a>.");
    }
}
