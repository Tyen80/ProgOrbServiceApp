using ServiceApp.Application.ToDoItems;
using ServiceApp.Domain.Abstractions;
using ServiceApp.WebUI.Client.Features.ToDoItems;

namespace ServiceApp.WebUI.Client.Services.ToDoItemService;

public interface IToDoItemService
{
    Task<Result<List<ToDoItemResponse>>> GetAllToDoItems();
    Task<Result<List<ToDoItemResponse>>> GetAllActiveToDoItems();
    Task<Result<ToDoItemModel?>> GetToDoItemById(int id);
    Task<Result> CreateToDoItem(ToDoItemModel toDoItem);
    Task<Result<ToDoItemModel>> UpdateToDoItem(ToDoItemModel toDoItem);
    Task<bool> DeleteToDoItem(int id);
}
