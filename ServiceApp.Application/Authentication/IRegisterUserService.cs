namespace ServiceApp.Application.Authentication;
public interface IRegisterUserService
{
    Task<RegisterUserResponse> RegisterUserAsync(string userName, string password, string familyId);
}
