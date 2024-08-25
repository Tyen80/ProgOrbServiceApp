using ServiceApp.Domain.Abstractions;
using ServiceApp.Domain.Tasks;

namespace ServiceApp.Domain.ToDoItems;
public class ToDoItem : Entity
{
    public int TaskId { get; set; }
    public bool IsComplete { get; set; } = false;
    public TaskToDo? Task { get; set; }

}
