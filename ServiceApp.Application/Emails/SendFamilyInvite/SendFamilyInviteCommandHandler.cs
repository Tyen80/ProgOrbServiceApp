
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Emails.SendFamilyInvite;
public class SendFamilyInviteCommandHandler : ICommandHandler<SendFamilyInviteCommand>
{
    private readonly ISendFamilyInviteService _sendFamilyInviteService;

    public SendFamilyInviteCommandHandler(ISendFamilyInviteService sendFamilyInviteService)
    {
        _sendFamilyInviteService = sendFamilyInviteService;
    }

    public async Task<Result> Handle(SendFamilyInviteCommand request, CancellationToken cancellationToken)
    {
        var result = await _sendFamilyInviteService.SendFamilyJoinInvite(request.Email, request.FamilyId, request.Role);
        if (result.Success)
        {
            return Result.Ok();
        }
        return Result.Fail($"{result.Error}");
    }
}
