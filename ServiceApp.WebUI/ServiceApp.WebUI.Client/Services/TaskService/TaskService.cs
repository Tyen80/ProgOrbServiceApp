using ServiceApp.Application.Tasks;
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

    public async Task<List<TaskResponse>> GetAllTasks()
    {
        var response = await _httpClient.GetFromJsonAsync<List<TaskResponse>>("api/task");
        return response ?? new List<TaskResponse>();
    }
    public async Task<List<TaskResponse>> GetAllTasksByUserId()
    {
        var response = await _httpClient.GetFromJsonAsync<List<TaskResponse>>("api/task/user");
        return response ?? new List<TaskResponse>();
    }

    public async Task<TaskResponse?> GetTaskById(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<TaskResponse>($"api/task/{id}");
        return response;
    }

    public async Task CreateTask(TaskToDoModel task)
    {
        var response = await _httpClient.PostAsJsonAsync("api/task", task);
        if (response == null)
        {
            throw new Exception("Failed to create the task");
        }
    }

    public async Task<TaskToDoModel> UpdateTask(TaskToDoModel task)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/task/{task.Id}", task);
        if (response == null)
        {
            throw new Exception("Failed to update the task");
        }
        return task;
    }

    public async Task<bool> DeleteTask(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/task/{id}");
        return response.IsSuccessStatusCode;
    }

}