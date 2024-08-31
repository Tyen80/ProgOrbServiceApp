namespace ServiceApp.Application.Users.RegisterUser;
public class RegisterInvitedUserCommand : ICommand
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string FamilyId { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

