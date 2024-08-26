using Mapster;
using ServiceApp.Application.Abstractions.RequestHandling;
using ServiceApp.Domain.Tasks;

namespace ServiceApp.Application.Tasks.GetTaskById;
public class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, TaskResponse>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<TaskResponse>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task is null)
        {
            return Result.Fail<TaskResponse>($"Task with id {request.Id} not found");
        }
        return Result.Ok(task.Adapt<TaskResponse>());
    }

}
