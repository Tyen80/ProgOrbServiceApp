namespace ServiceApp.Application.Users.RegisterNewUser;
public class RegisterNewUserCommand : ICommand
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string FamilyId { get; set; } = string.Empty;
}
