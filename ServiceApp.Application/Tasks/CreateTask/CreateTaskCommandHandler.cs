using Mapster;
using ServiceApp.Application.Abstractions.RequestHandling;
using ServiceApp.Domain.Tasks;

namespace ServiceApp.Application.Tasks.CreateTask;

public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, TaskResponse>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<TaskResponse>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
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