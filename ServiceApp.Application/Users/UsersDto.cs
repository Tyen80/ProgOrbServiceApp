using ServiceApp.Application.ToDoItems.ToDoItemsDtos;

namespace ServiceApp.Application.Users;
public class UsersDto
{
    public string Id { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FamilyId { get; set; }
    public List<ToDoItemResponse>? ToDoItems { get; set; }
}
