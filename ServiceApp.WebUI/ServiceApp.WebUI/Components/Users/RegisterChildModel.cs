using System.ComponentModel.DataAnnotations;

namespace ServiceApp.WebUI.Components.Users;

public class RegisterChildModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare("Password", ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? FamilyId { get; set; }

    public List<string> ChildIds { get; set; } = new();
}
