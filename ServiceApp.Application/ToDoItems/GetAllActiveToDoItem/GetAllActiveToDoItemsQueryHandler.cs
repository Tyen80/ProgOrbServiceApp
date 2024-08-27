using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.GetAllActiveToDoItem;
public class GetAllActiveToDoItemsQueryHandler : IQueryHandler<GetAllActiveToDoItemsQuery, List<ToDoItemResponse>>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public GetAllActiveToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<Result<List<ToDoItemResponse>>> Handle(GetAllActiveToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var toDoItems = await _toDoItemRepository.GetAllActiveAsync();
        var result = toDoItems.Adapt<List<ToDoItemResponse>>();
        return Result.Ok(result);
    }
}
