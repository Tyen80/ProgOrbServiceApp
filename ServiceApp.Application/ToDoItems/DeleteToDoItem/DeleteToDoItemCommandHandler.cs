
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.DeleteToDoItem;
public class DeleteToDoItemCommandHandler : ICommandHandler<DeleteToDoItemCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public DeleteToDoItemCommandHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<Result> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var toDoItemDeleted = await _toDoItemRepository.DeleteAsync(request.Id);
        if (toDoItemDeleted)
        {
            return Result.Ok();
        }
        return Result.Fail("ToDoItem not found");
    }
}
