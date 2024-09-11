namespace ServiceApp.Application.Emails.SendChildInvite;
public class SendChildInviteCommand : ICommand
{
    public string Email { get; set; } = string.Empty;
    public string FamilyId { get; set; } = string.Empty;
}
