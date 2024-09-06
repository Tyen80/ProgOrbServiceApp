using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Application.ToDoItems.CreateToDoItem;
using ServiceApp.Application.ToDoItems.DeleteToDoItem;
using ServiceApp.Application.ToDoItems.GetAllActiveToDoItem;
using ServiceApp.Application.ToDoItems.GetAllCompletedTaskForLastWeek;
using ServiceApp.Application.ToDoItems.GetPendingApprovelToDoItems;
using ServiceApp.Application.ToDoItems.GetToDoItemById;
using ServiceApp.Application.ToDoItems.GetTotalMoneyEarned;
using ServiceApp.Application.ToDoItems.ToDoItemsDtos;
using ServiceApp.Application.ToDoItems.UpdateToDoItem;
using ServiceApp.Domain.Abstractions;

namespace ServiceApp.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ToDoItemController : ControllerBase
{
    private readonly ISender _sender;

    public ToDoItemController(ISender sender)
    {
        _sender = sender;
    }

    //[HttpGet]
    //public async Task<ActionResult<Result<List<ToDoItemResponse>>>> GetAllToDoItems()
    //{
    //    var result = await _sender.Send(new GetAllToDoItemsQuery());
    //    return Ok(result);
    //}

    [HttpGet("active")]
    public async Task<ActionResult<Result<List<ToDoItemResponse>>>> GetAllActiveToDoItems()
    {
        var result = await _sender.Send(new GetAllActiveToDoItemsQuery());
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("total-money-earned")]
    public async Task<ActionResult<Result<TotalMoneyEarnedDto>>> GetTotalMoneyEarned()
    {
        var result = await _sender.Send(new GetTotalMoneyEarnedQuery());
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("completed-task-for-last-week")]
    public async Task<ActionResult<Result<List<CompletedTaskDto>>>> GetCompletedTaskForLastWeek()
    {
        var result = await _sender.Send(new GetCompletedTaskForLastWeekQuery());
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("pending-approval-tasks")]
    public async Task<ActionResult<Result<List<CompletedTaskDto>>>> GetPendingApprovalTasks()
    {
        var result = await _sender.Send(new GetPendingApprovelToDoItemsQuery());
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ToDoItemResponse>>> GetToDoItemById(int id)
    {
        var result = await _sender.Send(new GetToDoItemByIdQuery { Id = id });
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<ActionResult<Result>> CreateToDoItem(CreateToDoItemCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> UpdateToDoItem(int id, UpdateToDoItemCommand command)
    {
        command.Id = id;
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> DeleteToDoItem(int id)
    {
        var result = await _sender.Send(new DeleteToDoItemCommand { Id = id });
        return Ok(result);
    }


}
