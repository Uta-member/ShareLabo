using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.Follow
{
    public interface IFollowDeleteCommandService : ICommandService<IFollowDeleteCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required FollowIdentifierDTO FollowId { get; init; }

            public required OperateInfoWriteModel OperateInfo { get; init; }
        }
    }
}
