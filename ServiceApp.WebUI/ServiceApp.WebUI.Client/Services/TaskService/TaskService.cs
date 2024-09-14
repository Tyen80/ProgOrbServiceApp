using ServiceApp.Application.Tasks;
using ServiceApp.Domain.Abstractions;
using ServiceApp.WebUI.Client.Features.Tasks;
using System.Net.Http.Json;

namespace ServiceApp.WebUI.Client.Services.TaskService;

public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;

    public TaskService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<List<TaskResponse>>> GetAllTasks()
    {
        var response = await _httpClient.GetFromJsonAsync<List<TaskResponse>>("api/task");
        if (response == null)
        {
            return Result.Fail<List<TaskResponse>>("Tasks not found");
        }
        return Result.Ok(response);
    }
    public async Task<Result<List<TaskResponse>>> GetAllTasksByUserId()
    {
        var response = await _httpClient.GetFromJsonAsync<List<TaskResponse>>("api/task/user");
        if (response == null)
        {
            return Result.Fail<List<TaskResponse>>("Tasks not found");
        }
        return Result.Ok(response);
    }

    public async Task<Result<TaskToDoModel?>> GetTaskById(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<TaskToDoModel>($"api/task/{id}");
        if (response == null)
        {
            return Result.Fail<TaskToDoModel?>($"Task with id {id} not found");
        }
        return Result.Ok<TaskToDoModel?>(response);
    }

    public async Task<Result> CreateTask(TaskToDoModel task)
    {
        var response = await _httpClient.PostAsJsonAsync("api/task", task);
        if (response.IsSuccessStatusCode)
        {
            return Result.Ok();
        }
        return Result.Fail("Failed to create the task");
    }

    public async Task<Result<TaskToDoModel>> UpdateTask(TaskToDoModel task)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/task/{task.Id}", task);
        if (response.IsSuccessStatusCode)
        {
            return Result.Ok(task);
        }
        return Result.Fail<TaskToDoModel>("Failed to update the task");
    }

    public async Task<bool> DeleteTask(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/task/{id}");
        return response.IsSuccessStatusCode;
    }


}