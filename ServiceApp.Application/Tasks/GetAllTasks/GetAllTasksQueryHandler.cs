using Mapster;
using ServiceApp.Application.Abstractions.RequestHandling;
using ServiceApp.Domain.Tasks;

namespace ServiceApp.Application.Tasks.GetAllTasks;
public class GetAllTasksQueryHandler : IQueryHandler<GetAllTasksQuery, List<TaskResponse>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<List<TaskResponse>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync();
        var result = tasks.Adapt<List<TaskResponse>>();
        return Result.Ok(result);
    }
}
