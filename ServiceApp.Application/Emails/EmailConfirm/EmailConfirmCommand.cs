namespace ServiceApp.Application.Emails.EmailConfirm;
public class EmailConfirmCommand : ICommand
{
    public required string UserId { get; set; }
    public required string Token { get; set; }
}
