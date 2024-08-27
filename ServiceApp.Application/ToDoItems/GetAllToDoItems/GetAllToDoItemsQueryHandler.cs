using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.GetAllToDoItems;
public class GetAllToDoItemsQueryHandler : IQueryHandler<GetAllToDoItemsQuery, List<ToDoItemResponse>>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public GetAllToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<Result<List<ToDoItemResponse>>> Handle(GetAllToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var toDoItems = await _toDoItemRepository.GetAllAsync();
        var result = toDoItems.Adapt<List<ToDoItemResponse>>();
        return Result.Ok(result);
    }
}
