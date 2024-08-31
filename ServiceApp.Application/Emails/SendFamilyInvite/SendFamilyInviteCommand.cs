namespace ServiceApp.Application.Emails.SendFamilyInvite;
public class SendFamilyInviteCommand : ICommand
{
    public string Email { get; set; } = string.Empty;
    public string FamilyId { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
