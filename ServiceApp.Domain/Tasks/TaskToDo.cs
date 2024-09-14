using ServiceApp.Domain.Abstractions;

namespace ServiceApp.Domain.Tasks;
public class TaskToDo : Entity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public string UserId { get; set; } = string.Empty;
}
