using ServiceApp.Application.Authentication;
using ServiceApp.Domain.Users;

namespace ServiceApp.Application.Users.GetAllFamilyUsers;
public class GetAllFamilyUsersQueryHandler : ICommandHandler<GetAllFamilyUsersQuery, List<UsersDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IUserRolesService _userRolesService;

    public GetAllFamilyUsersQueryHandler(IUserRepository userRepository, IUserService userService, IUserRolesService userRolesService)
    {
        _userRepository = userRepository;
        _userService = userService;
        _userRolesService = userRolesService;
    }

    public async Task<Result<List<UsersDto>>> Handle(GetAllFamilyUsersQuery request, CancellationToken cancellationToken)
    {
        if (!await _userService.IsCurrentUserInRoleAsync("FamilyAdmin"))
        {
            return Result.Fail<List<UsersDto>>("User is not authorized to perform this action");
        }

        var familyId = await _userService.GetCurrentFamilyIdAsync();
        var users = await _userRepository.GetAllUsersByFamilyIdWithRolesAsync();

        var userWithFamilyId = users.Where(u => u.FamilyId == familyId).ToList();

        var response = new List<UsersDto>();
        foreach (var user in userWithFamilyId)
        {
            var userResponse = user.Adapt<UsersDto>();
            userResponse.Roles = string.Join(" , ", await _userRolesService.GetUserRolesAsync(user.Id));
            response.Add(userResponse);
        }
        return Result.Ok(response);
    }
}

