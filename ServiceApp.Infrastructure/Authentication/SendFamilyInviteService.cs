using Microsoft.Extensions.Configuration;
using ServiceApp.Application.Authentication;
using ServiceApp.Domain.Abstractions;
using ServiceApp.Domain.Email;

namespace ServiceApp.Infrastructure.Authentication;
public class SendFamilyInviteService : ISendFamilyInviteService
{
    private readonly IEmailService _emailService;
    private readonly IFamilyExistService _familyExistService;
    private readonly IConfiguration _config;

    public SendFamilyInviteService(IEmailService emailService, IFamilyExistService familyExistService, IConfiguration config)
    {
        _emailService = emailService;
        _familyExistService = familyExistService;
        _config = config;
    }

    public async Task<Result> SendFamilyJoinInvite(string email, string familyId, string role)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(familyId) || string.IsNullOrWhiteSpace(role))
        {
            return Result.Fail("Email, family ID or role is empty");
        }

        var familyExists = await _familyExistService.FamilyExists(familyId);

        if (!familyExists)
        {
            return Result.Fail("Family does not exist");
        }

        var subject = "Join my family!";
        var baseUrl = _config["ApplicationSettings:BaseUrl"];
        var familyUrl = $"{baseUrl}/Family/Join?familyId={familyId}&role={role}";
        var message = $"You have been invited to join a family as a {role}! Click <a href='" + familyUrl + "'>here</a> to join the family.";

        try
        {
            await _emailService.SendEmailAsync(email, subject, message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Fail("Failed to send the email invite due to an unexpected error.");
        }
        return Result.Ok();
    }
}
