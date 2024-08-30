using ServiceApp.Domain.Abstractions;
using ServiceApp.Domain.Tasks;

namespace ServiceApp.Domain.ToDoItems;
public class ToDoItem : Entity
{
    public int TaskId { get; set; }
    public bool IsComplete { get; set; } = false;
    public DateTime DueDate { get; set; } = DateTime.Now;
    public TaskToDo? Task { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FamilyId { get; set; } = string.Empty;

}
