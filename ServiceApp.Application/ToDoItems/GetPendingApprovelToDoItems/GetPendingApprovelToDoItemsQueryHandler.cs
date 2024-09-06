using ServiceApp.Application.ToDoItems.ToDoItemsDtos;
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.GetPendingApprovelToDoItems;
public class GetPendingApprovelToDoItemsQueryHandler : IQueryHandler<GetPendingApprovelToDoItemsQuery, List<CompletedTaskDto>>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public GetPendingApprovelToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<Result<List<CompletedTaskDto>>> Handle(GetPendingApprovelToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var response = await _toDoItemRepository.GetPendingApprovalTasks();
        var result = response.Adapt<List<CompletedTaskDto>>();
        return Result.Ok(result);
    }
}
