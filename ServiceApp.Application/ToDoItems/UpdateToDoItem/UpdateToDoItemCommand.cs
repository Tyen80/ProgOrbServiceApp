using ServiceApp.Application.ToDoItems.ToDoItemsDtos;

namespace ServiceApp.Application.ToDoItems.UpdateToDoItem;
public class UpdateToDoItemCommand : ICommand<ToDoItemResponse?>
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public bool IsComplete { get; set; }
    public bool IsApproved { get; set; }
    public DateTime DueDate { get; set; }
}
