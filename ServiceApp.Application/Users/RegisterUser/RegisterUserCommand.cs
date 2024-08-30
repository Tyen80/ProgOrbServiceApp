namespace ServiceApp.Application.Users.RegisterUser;
public class RegisterUserCommand : ICommand
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

