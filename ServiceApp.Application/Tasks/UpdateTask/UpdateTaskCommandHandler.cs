
namespace ServiceApp.Application.Tasks.UpdateTask;
public class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand, TaskResponse?>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<TaskResponse?>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var updatedTask = request.Adapt<TaskToDo>();

        var task = await _taskRepository.UpdateAsync(updatedTask);
        if (task != null)
        {
            return task.Adapt<TaskResponse>();
        }
        return Result.Fail<TaskResponse?>("Task not found");
    }
}
