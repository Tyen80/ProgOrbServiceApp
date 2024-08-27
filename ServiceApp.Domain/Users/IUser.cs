using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Domain.Users;
public interface IUser
{
    public string Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<ToDoItem> ToDoItems { get; set; }
}
