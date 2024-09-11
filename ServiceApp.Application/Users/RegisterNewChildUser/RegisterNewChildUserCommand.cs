namespace ServiceApp.Application.Users.RegisterNewChildUser;
public class RegisterNewChildUserCommand : ICommand
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string FamilyId { get; set; } = string.Empty;
    public List<string> ChildIds { get; set; } = new();
}
