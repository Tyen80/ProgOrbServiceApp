namespace ServiceApp.Application.Authentication;
public interface ISendChildInviteService
{
    Task<Result> SendChildJoinInvite(string email, string familyId);
}
