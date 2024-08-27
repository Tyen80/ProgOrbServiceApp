using ServiceApp.Domain.Tasks;

namespace ServiceApp.WebUI.Client.Features.ToDoItems;

public class ToDoItemModel
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public bool IsComplete { get; set; } = false;
    public DateTime DueDate { get; set; } = DateTime.Now;
    public TaskToDo? Task;
}
