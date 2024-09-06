using ServiceApp.Application.ToDoItems.ToDoItemsDtos;
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.GetAllCompletedTaskForLastWeek;
public class GetCompletedTaskForLastWeekQueryHandler : IQueryHandler<GetCompletedTaskForLastWeekQuery, List<CompletedTaskDto>>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public GetCompletedTaskForLastWeekQueryHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<Result<List<CompletedTaskDto>>> Handle(GetCompletedTaskForLastWeekQuery request, CancellationToken cancellationToken)
    {
        var completedTaskResult = await _toDoItemRepository.GetCompletedTaskTitle();

        var completedTaskDtos = completedTaskResult
                 .Where(x => x.Task != null)
                 .Select(x => new CompletedTaskDto
                 {
                     Title = x.Task!.Title,
                     Amount = x.Task.Amount,
                     DateUpdated = x.DateUpdated
                 }).ToList();

        return Result.Ok(completedTaskDtos); ;
    }
}

