namespace ServiceApp.Application.Tasks.CreateTask;
public class CreateTaskCommand : ICommand<TaskResponse>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public string UserId { get; set; } = string.Empty;
}