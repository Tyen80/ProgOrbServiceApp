using Microsoft.AspNetCore.WebUtilities;
using ServiceApp.Application.Authentication;
using System.Text;

namespace ServiceApp.Application.Emails.EmailConfirm;
public class EmailConfirmCommandHandler : ICommandHandler<EmailConfirmCommand>
{
    private readonly IEmailConfirmService _emailConfirmService;
    private readonly IUserService _userService;

    public EmailConfirmCommandHandler(IEmailConfirmService emailConfirmService, IUserService userService)
    {
        _emailConfirmService = emailConfirmService;
        _userService = userService;
    }


    public async Task<Result> Handle(EmailConfirmCommand request, CancellationToken cancellationToken)
    {

        var user = await _userService.GetUserId(request.UserId);
        if (user == null)
        {
            return Result.Fail("User not found");
        }

        try
        {
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _emailConfirmService.ConfirmEmail(user, decodedToken);

            return result ? Result.Ok() : Result.Fail("Email confirmation failed");
        }
        catch (FormatException)
        {
            return Result.Fail("Invalid token format");
        }
    }
}
