namespace ServiceApp.Application.ToDoItems.CreateToDoItem;
public class CreateToDoItemCommand : ICommand<ToDoItemResponse>
{
    public int TaskId { get; set; }
    public bool IsComplete { get; set; } = false;
    public string? UserId { get; set; }
}
