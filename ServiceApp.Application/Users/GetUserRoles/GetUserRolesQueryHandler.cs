
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Users.GetUserRoles;
public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, List<string>>
{
    private readonly IUserRolesService _userRolesService;

    public GetUserRolesQueryHandler(IUserRolesService userRolesService)
    {
        _userRolesService = userRolesService;
    }

    public async Task<Result<List<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _userRolesService.GetRoleByUserId(request.UserId);
        return roles;
    }
}
