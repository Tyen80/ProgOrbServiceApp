
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Users.RegisterNewUser;
public class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand>
{
    private readonly IRegisterUserService _registerUserService;

    public RegisterNewUserCommandHandler(IRegisterUserService registerUserService)
    {
        _registerUserService = registerUserService;
    }

    public async Task<Result> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _registerUserService.RegisterNewUserAsync(request.UserName, request.Email, request.Password, request.FamilyId);
        if (result.Succeeded)
        {
            return Result.Ok();
        }
        return Result.Fail($"{string.Join(" , ", result.Errors)}");
    }
}