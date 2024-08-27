
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.UpdateToDoItem;
public class UpdateToDoItemCommandHandler : ICommandHandler<UpdateToDoItemCommand, ToDoItemResponse?>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public UpdateToDoItemCommandHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<Result<ToDoItemResponse?>> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var updatedToDoItem = request.Adapt<ToDoItem>();
        var toDoItem = await _toDoItemRepository.UpdateAsync(updatedToDoItem);
        if (toDoItem != null)
        {
            return toDoItem.Adapt<ToDoItemResponse>();
        }
        return Result.Fail<ToDoItemResponse?>("ToDoItem not found");
    }
}
