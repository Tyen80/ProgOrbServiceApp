using ServiceApp.Application.Tasks;
using ServiceApp.WebUI.Client.Features.Tasks;

namespace ServiceApp.WebUI.Client.Services.TaskService;

public interface ITaskService
{
    Task<List<TaskResponse>> GetAllTasks();
    Task<List<TaskResponse>> GetAllTasksByUserId();
    Task<TaskResponse?> GetTaskById(int id);
    Task CreateTask(TaskToDoModel task);
    Task<TaskToDoModel> UpdateTask(TaskToDoModel task);
    Task<bool> DeleteTask(int id);
}
