using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Tasks.CreateTask;

public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, TaskResponse>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserService _userService;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IUserService userService)
    {
        _taskRepository = taskRepository;
        _userService = userService;
    }

    public async Task<Result<TaskResponse>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetCurrentUserByIdAsync();
        request.UserId = userId;

        try
        {
            var newTask = request.Adapt<TaskToDo>();
            var createdTAsk = await _taskRepository.CreateAsync(newTask);
            return createdTAsk.Adapt<TaskResponse>();
        }
        catch (Exception)
        {
            return Result<TaskResponse>.Fail<TaskResponse>("Task not created");
        }
    }
}