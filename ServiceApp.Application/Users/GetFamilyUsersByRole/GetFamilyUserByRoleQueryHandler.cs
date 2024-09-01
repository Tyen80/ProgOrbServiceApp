using ServiceApp.Application.Authentication;
using ServiceApp.Domain.Users;

namespace ServiceApp.Application.Users.GetFamilyUsersByRole;
public class GetFamilyUserByRoleQueryHandler : ICommandHandler<GetFamilyUserByRoleQuery, List<IUser>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public GetFamilyUserByRoleQueryHandler(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<Result<List<IUser>>> Handle(GetFamilyUserByRoleQuery request, CancellationToken cancellationToken)
    {
        var familyId = await _userService.GetCurrentFamilyIdAsync();
        var users = await _userRepository.GetFamilyMembersAsync(familyId);
        return Result.Ok(users);
    }
}
