using ServiceApp.Domain.Tasks;

namespace ServiceApp.WebUI.Client.Features.ToDoItems;

public class ToDoItemModel
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public bool IsComplete { get; set; } = false;
    public bool IsApproved { get; set; } = false;
    public string UserId { get; set; } = string.Empty;
    public string FamilyId { get; set; } = string.Empty;
    public DateTime DueDate { get; set; } = DateTime.Now;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateUpdated { get; set; }
    public TaskToDo? Task;

    public string? UserName { get; set; }
}
