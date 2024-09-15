using ServiceApp.Application.Authentication;
using ServiceApp.Application.ToDoItems.ToDoItemsDtos;
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.GetAllCompletedTaskForLastWeek;
public class GetCompletedTaskForLastWeekQueryHandler : IQueryHandler<GetCompletedTaskForLastWeekQuery, List<CompletedTaskDto>>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUserService _userService;

    public GetCompletedTaskForLastWeekQueryHandler(IToDoItemRepository toDoItemRepository, IUserService userService)
    {
        _toDoItemRepository = toDoItemRepository;
        _userService = userService;
    }

    public async Task<Result<List<CompletedTaskDto>>> Handle(GetCompletedTaskForLastWeekQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetCurrentUserByIdAsync();
        var familyId = await _userService.GetCurrentFamilyIdAsync();
        var isFamilyAdmin = await _userService.IsCurrentUserInRoleAsync("FamilyAdmin");

        if (isFamilyAdmin)
        {
            return await GetCompletedTaskForLastWeek(familyId);
        }
        else
        {
            return await GetCompletedTaskForOneUserId(userId);
        }
    }

    private async Task<Result<List<CompletedTaskDto>>> GetCompletedTaskForLastWeek(string familyId)
    {
        return await GetCompletedTasks(x => x.FamilyId == familyId);
    }

    private async Task<Result<List<CompletedTaskDto>>> GetCompletedTaskForOneUserId(string userId)
    {
        return await GetCompletedTasks(x => x.UserId == userId);
    }

    private async Task<Result<List<CompletedTaskDto>>> GetCompletedTasks(Func<ToDoItem, bool> predicate)
    {
        var completedTasks = await _toDoItemRepository.GetCompletedTaskTitle();
        var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
        var completedTaskDtos = new List<CompletedTaskDto>();

        foreach (var task in completedTasks.Where(x => x.Task != null && x.IsApproved && predicate(x) && x.DateUpdated >= oneWeekAgo))
        {
            var userName = await _userService.GetUserNameById(task.UserId);
            completedTaskDtos.Add(new CompletedTaskDto
            {
                Title = task.Task!.Title,
                Amount = task.Task.Amount,
                DateUpdated = task.DateUpdated,
                UserName = userName
            });
        }

        return Result.Ok(completedTaskDtos);
    }
}

