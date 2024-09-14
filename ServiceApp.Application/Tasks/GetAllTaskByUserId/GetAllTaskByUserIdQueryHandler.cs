using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Tasks.GetAllTaskByUserId;
public class GetAllTaskByUserIdQueryHandler : IQueryHandler<GetAllTaskByUserIdQuery, List<TaskResponse>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserService _userService;

    public GetAllTaskByUserIdQueryHandler(ITaskRepository taskRepository, IUserService userService)
    {
        _taskRepository = taskRepository;
        _userService = userService;
    }

    public async Task<Result<List<TaskResponse>>> Handle(GetAllTaskByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetCurrentUserByIdAsync();
        var tasks = await _taskRepository.GetAllAsync();

        var userTasks = tasks.Where(task => task.UserId == userId).ToList();

        if (userTasks == null || !userTasks.Any())
        {
            return Result.Fail<List<TaskResponse>>("No tasks found for the user.");
        }

        var result = userTasks.Adapt<List<TaskResponse>>();

        return Result.Ok(result);
    }
}

