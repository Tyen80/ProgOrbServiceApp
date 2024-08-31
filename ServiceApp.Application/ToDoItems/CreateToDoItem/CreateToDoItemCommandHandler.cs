
using ServiceApp.Application.Authentication;
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.CreateToDoItem;
public class CreateToDoItemCommandHandler : ICommandHandler<CreateToDoItemCommand, ToDoItemResponse>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUserService _userService;

    public CreateToDoItemCommandHandler(IToDoItemRepository toDoItemRepository, IUserService userService)
    {
        _toDoItemRepository = toDoItemRepository;
        _userService = userService;
    }

    public async Task<Result<ToDoItemResponse>> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newToDoItem = request.Adapt<ToDoItem>();

            newToDoItem.UserId = request.UserId ?? await _userService.GetCurrentUserByIdAsync();
            newToDoItem.FamilyId = await _userService.GetCurrentFamilyIdAsync();

            var createdToDoItem = await _toDoItemRepository.CreateAsync(newToDoItem);
            return createdToDoItem.Adapt<ToDoItemResponse>();
        }
        catch (Exception)
        {
            return Result<ToDoItemResponse>.Fail<ToDoItemResponse>("ToDoItem not created");
        }
    }
}
