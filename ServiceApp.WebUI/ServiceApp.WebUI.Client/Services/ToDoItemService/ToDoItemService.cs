using Mapster;
using ServiceApp.Application.ToDoItems;
using ServiceApp.Domain.Abstractions;
using ServiceApp.WebUI.Client.Features.ToDoItems;
using System.Net.Http.Json;

namespace ServiceApp.WebUI.Client.Services.ToDoItemService;

public class ToDoItemService : IToDoItemService
{
    private readonly HttpClient _http;

    public ToDoItemService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Result<List<ToDoItemResponse>>> GetAllToDoItems()
    {
        var response = await _http.GetFromJsonAsync<List<ToDoItemResponse>>("api/todoitem");
        if (response == null)
        {
            return Result.Fail<List<ToDoItemResponse>>("ToDoItems not found");
        }
        return Result.Ok(response);
    }

    public async Task<Result<List<ToDoItemResponse>>> GetAllActiveToDoItems()
    {
        var response = await _http.GetFromJsonAsync<List<ToDoItemResponse>>("api/todoitem/active");
        if (response == null)
        {
            return Result.Fail<List<ToDoItemResponse>>("Active ToDoItems not found");
        }
        return Result.Ok(response);
    }

    public async Task<Result<ToDoItemModel?>> GetToDoItemById(int id)
    {
        var response = await _http.GetFromJsonAsync<ToDoItemResponse?>($"api/todoitem/{id}");
        if (response == null)
        {
            return Result.Fail<ToDoItemModel?>($"ToDoItem with id {id} not found");
        }
        var toDoItemModel = response.Adapt<ToDoItemModel>();
        return Result.Ok<ToDoItemModel?>(toDoItemModel);
    }

    public async Task<Result> CreateToDoItem(ToDoItemModel toDoItem)
    {
        var response = await _http.PostAsJsonAsync("api/todoitem", toDoItem);
        if (response.IsSuccessStatusCode)
        {
            return Result.Ok();
        }
        return Result.Fail("Failed to create the ToDoItem");
    }

    public async Task<Result<ToDoItemModel>> UpdateToDoItem(ToDoItemModel toDoItem)
    {
        var response = await _http.PutAsJsonAsync($"api/todoitem/{toDoItem.Id}", toDoItem);
        if (response.IsSuccessStatusCode)
        {
            return Result.Ok(toDoItem);
        }
        return Result.Fail<ToDoItemModel>("Failed to update the ToDoItem");
    }

    public async Task<bool> DeleteToDoItem(int id)
    {
        var response = await _http.DeleteAsync($"api/todoitem/{id}");
        return response.IsSuccessStatusCode;
    }


}
