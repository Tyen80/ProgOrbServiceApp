using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Users.RegisterNewChildUser;
public class RegisterNewChildUserCommandHandler : ICommandHandler<RegisterNewChildUserCommand>
{
    private readonly IRegisterUserService _registerUserService;

    public RegisterNewChildUserCommandHandler(IRegisterUserService registerUserService)
    {
        _registerUserService = registerUserService;
    }

    public async Task<Result> Handle(RegisterNewChildUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _registerUserService.RegisterNewChildUserAsync(request.UserName, request.Email, request.Password, request.FamilyId, request.ChildIds);
        if (result.Succeeded)
        {
            return Result.Ok();
        }
        return Result.Fail($"{string.Join(" , ", result.Errors)}");
    }
}
