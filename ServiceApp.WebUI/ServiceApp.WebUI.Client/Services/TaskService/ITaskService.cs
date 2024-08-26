using ServiceApp.Application.Tasks;
using ServiceApp.Domain.Abstractions;
using ServiceApp.WebUI.Client.Features.Tasks;

namespace ServiceApp.WebUI.Client.Services.TaskService;

public interface ITaskService
{
    Task<Result<List<TaskResponse>>> GetAllTasks();
    Task<Result<TaskToDoModel?>> GetTaskById(int id);
    Task<Result> CreateTask(TaskToDoModel task);
    Task<Result<TaskToDoModel>> UpdateTask(TaskToDoModel task);
    Task<bool> DeleteTask(int id);
}
