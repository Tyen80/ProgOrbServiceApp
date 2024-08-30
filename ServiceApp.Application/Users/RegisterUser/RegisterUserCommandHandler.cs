
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Users.RegisterUser;
public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IRegisterUserService _registerUserService;

    public RegisterUserCommandHandler(IRegisterUserService registerUserService)
    {
        _registerUserService = registerUserService;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _registerUserService.RegisterUserAsync(request.UserName, request.Email, request.Password);
        if (result.Succeeded)
        {
            return Result.Ok();
        }
        return Result.Fail($"{string.Join(" , ", result.Errors)}");
    }
}
