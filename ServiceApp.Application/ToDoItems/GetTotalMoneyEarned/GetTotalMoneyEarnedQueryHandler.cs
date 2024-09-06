using ServiceApp.Application.Authentication;
using ServiceApp.Application.ToDoItems.ToDoItemsDtos;
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.GetTotalMoneyEarned;
public class GetTotalMoneyEarnedQueryHandler : IQueryHandler<GetTotalMoneyEarnedQuery, TotalMoneyEarnedDto>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUserService _userService;

    public GetTotalMoneyEarnedQueryHandler(IToDoItemRepository toDoItemRepository, IUserService userService)
    {
        _toDoItemRepository = toDoItemRepository;
        _userService = userService;
    }

    async Task<Result<TotalMoneyEarnedDto>> IRequestHandler<GetTotalMoneyEarnedQuery, Result<TotalMoneyEarnedDto>>.Handle(GetTotalMoneyEarnedQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetCurrentUserByIdAsync();
        var (total, totalPerWeek, totalPerMonth) = await _toDoItemRepository.GetTotalMoneyEarned(userId);

        var totalMoneyEarnedDto = new TotalMoneyEarnedDto
        {
            Total = total,
            TotalPerWeek = totalPerWeek,
            TotalPerMonth = totalPerMonth
        };
        return Result.Ok(totalMoneyEarnedDto);
    }
}
