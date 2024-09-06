namespace ServiceApp.Domain.ToDoItems;

public interface IToDoItemRepository : IRepository<ToDoItem>
{
    Task<List<ToDoItem>> GetAllActiveAsync();
    Task<List<ToDoItem>> GetAllActiveByFamilyId(string FamilyId);
    Task<List<ToDoItem>> GetAllActiveByUserId(string UserId);
    Task<(decimal total, decimal totalPerWeek, decimal totalPerMonth)> GetTotalMoneyEarned(string userId);
    Task<List<ToDoItem>> GetCompletedTaskTitle();
    Task<List<ToDoItem>> GetPendingApprovalTasks();
}
