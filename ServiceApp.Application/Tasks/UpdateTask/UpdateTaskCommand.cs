namespace ServiceApp.Application.Tasks.UpdateTask;
public class UpdateTaskCommand : ICommand<TaskResponse?>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
}
