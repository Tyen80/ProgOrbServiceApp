
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Users.LoginUser;
public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
{
    private readonly ILoginUserService _loginUserService;

    public LoginUserCommandHandler(ILoginUserService loginUserService)
    {
        _loginUserService = loginUserService;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var success = await _loginUserService.LoginAsync(request.UserName, request.Password);
        if (success)
        {
            return Result.Ok();
        }
        return Result.Fail("Invalid username or password");
    }
}
