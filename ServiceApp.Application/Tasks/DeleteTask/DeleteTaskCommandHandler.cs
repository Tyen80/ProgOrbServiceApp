
using ServiceApp.Domain.Tasks;

namespace ServiceApp.Application.Tasks.DeleteTask;
public class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _taskRepository.DeleteAsync(request.Id);
        if (deleted)
        {
            return Result.Ok();
        }
        return Result.Fail("Task not found");
    }
}
