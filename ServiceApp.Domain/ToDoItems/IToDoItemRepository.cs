namespace ServiceApp.Domain.ToDoItems;

public interface IToDoItemRepository : IRepository<ToDoItem>
{
    Task<List<ToDoItem>> GetAllActiveAsync();
    Task<List<ToDoItem>> GetAllActiveByFamilyId(string FamilyId);
    Task<List<ToDoItem>> GetAllActiveByUserId(string UserId);

}
