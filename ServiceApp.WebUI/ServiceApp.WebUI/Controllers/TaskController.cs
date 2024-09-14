using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Application.Tasks;
using ServiceApp.Application.Tasks.CreateTask;
using ServiceApp.Application.Tasks.DeleteTask;
using ServiceApp.Application.Tasks.GetAllTaskByUserId;
using ServiceApp.Application.Tasks.GetAllTasks;
using ServiceApp.Application.Tasks.GetTaskById;
using ServiceApp.Application.Tasks.UpdateTask;
using ServiceApp.Domain.Abstractions;
using ServiceApp.WebUI.Client.Features.Tasks;

namespace ServiceApp.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ISender _sender;

    public TaskController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<TaskResponse>>>> GetAllTasks()
    {
        var result = await _sender.Send(new GetAllTasksQuery());
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("User/")]
    public async Task<ActionResult<Result<List<TaskResponse>>>> GetAllTasksByUserId()
    {
        var result = await _sender.Send(new GetAllTaskByUserIdQuery());
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<TaskToDoModel>>> GetTaskById(int id)
    {
        var result = await _sender.Send(new GetTaskByIdQuery { Id = id });
        if (result.Success)
        {
            var taskToDoModel = result.Value.Adapt<TaskToDoModel>();
            return Ok(taskToDoModel);
        }
        return BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTask(TaskToDoModel task)
    {
        var command = task.Adapt<CreateTaskCommand>();
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTask(int id, TaskToDoModel task)
    {
        var command = task.Adapt<UpdateTaskCommand>();
        command.Id = id;
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var command = new DeleteTaskCommand { Id = id };
        var result = await _sender.Send(command);
        return Ok(result);
    }

}