
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.GetToDoItemById;
public class GetToDoItemByIdQueryHandler : IQueryHandler<GetToDoItemByIdQuery, ToDoItemResponse>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public GetToDoItemByIdQueryHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<Result<ToDoItemResponse>> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoItemRepository.GetByIdAsync(request.Id);
        if (toDoItem is null)
        {
            return Result.Fail<ToDoItemResponse>($"ToDoItem with id {request.Id} not found");
        }
        return Result.Ok(toDoItem.Adapt<ToDoItemResponse>());
    }
}
