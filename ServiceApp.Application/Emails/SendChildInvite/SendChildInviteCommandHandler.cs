
using ServiceApp.Application.Authentication;

namespace ServiceApp.Application.Emails.SendChildInvite;
public class SendChildInviteCommandHandler : ICommandHandler<SendChildInviteCommand>
{
    private readonly ISendChildInviteService _sendChildInviteService;

    public SendChildInviteCommandHandler(ISendChildInviteService sendChildInviteService)
    {
        _sendChildInviteService = sendChildInviteService;
    }

    public async Task<Result> Handle(SendChildInviteCommand request, CancellationToken cancellationToken)
    {
        var result = await _sendChildInviteService.SendChildJoinInvite(request.Email, request.FamilyId);
        if (result.Success)
        {
            return Result.Ok();
        }
        return Result.Fail($"{result.Error}");
    }
}
