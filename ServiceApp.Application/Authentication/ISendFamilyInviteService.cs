namespace ServiceApp.Application.Authentication;
public interface ISendFamilyInviteService
{
    Task<Result> SendFamilyJoinInvite(string email, string familyId, string role);
}
