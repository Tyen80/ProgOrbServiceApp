
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Users.AddRoleToUser;
public class AddRoleToUserCommandHandler : ICommandHandler<AddRoleToUserCommand>
{
    private readonly IUserRolesService _userRolesService;

    public AddRoleToUserCommandHandler(IUserRolesService userRolesService)
    {
        _userRolesService = userRolesService;
    }

    public async Task<Result> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _userRolesService.AddRoleToTheUserAsync(request.UserId, request.RoleName);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}
