
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Users.RemoveRoleFromUser;
public class RemoveRoleFromUserCommandHandler : ICommandHandler<RemoveRoleFromUserCommand>
{
    private readonly IUserRolesService _userRolesService;

    public RemoveRoleFromUserCommandHandler(IUserRolesService userRolesService)
    {
        _userRolesService = userRolesService;
    }

    public async Task<Result> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _userRolesService.RemoveRoleFromUserAsync(request.UserId, request.RoleName);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}
