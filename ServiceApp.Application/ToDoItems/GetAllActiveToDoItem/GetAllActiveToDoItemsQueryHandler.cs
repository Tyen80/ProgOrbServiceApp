using ServiceApp.Application.Authentication;
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.GetAllActiveToDoItem;
public class GetAllActiveToDoItemsQueryHandler : IQueryHandler<GetAllActiveToDoItemsQuery, List<ToDoItemResponse>>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUserService _userService;

    public GetAllActiveToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository, IUserService userService)
    {
        _toDoItemRepository = toDoItemRepository;
        _userService = userService;
    }

    public async Task<Result<List<ToDoItemResponse>>> Handle(GetAllActiveToDoItemsQuery request, CancellationToken cancellationToken)
    {

        var userId = await _userService.GetCurrentUserByIdAsync();
        var isParent = await _userService.IsCurrentUserInToleAsync("Parent");

        List<ToDoItem> toDoItems;

        if (isParent)
        {
            var familyId = await _userService.GetCurrentFamilyIdAsync();
            toDoItems = await _toDoItemRepository.GetAllActiveByFamilyId(familyId);
        }
        else
        {
            toDoItems = await _toDoItemRepository.GetAllActiveByUserId(userId);
        }

        var result = toDoItems.Adapt<List<ToDoItemResponse>>();
        return Result.Ok(result);
    }
}
