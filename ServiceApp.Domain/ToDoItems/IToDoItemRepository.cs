namespace ServiceApp.Domain.ToDoItems;

public interface IToDoItemRepository : IRepository<ToDoItem>
{
    Task<List<ToDoItem>> GetAllActiveAsync();

}
