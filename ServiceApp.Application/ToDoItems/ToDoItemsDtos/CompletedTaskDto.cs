namespace ServiceApp.Application.ToDoItems.ToDoItemsDtos;
public class CompletedTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }

    public string? UserName { get; set; }

    public DateTime? DateUpdated { get; set; }
}
