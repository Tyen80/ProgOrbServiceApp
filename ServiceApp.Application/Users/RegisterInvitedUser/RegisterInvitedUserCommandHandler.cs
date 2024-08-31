
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Users.RegisterUser;
public class RegisterInvitedUserCommandHandler : ICommandHandler<RegisterInvitedUserCommand>
{
    private readonly IRegisterUserService _registerUserService;

    public RegisterInvitedUserCommandHandler(IRegisterUserService registerUserService)
    {
        _registerUserService = registerUserService;
    }

    public async Task<Result> Handle(RegisterInvitedUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _registerUserService.RegisterInvitedUserAsync(request.UserName, request.Email, request.Password, request.FamilyId, request.Role);
        if (result.Succeeded)
        {
            return Result.Ok();
        }
        return Result.Fail($"{string.Join(" , ", result.Errors)}");
    }
}
