using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.Follow
{
    public interface IFollowCreateCommandService : ICommandService<IFollowCreateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required FollowIdentifierDTO FollowId { get; init; }

            public required DateTime FollowStartDateTime { get; init; }

            public required OperateInfoWriteModel OperateInfo { get; init; }
        }
    }
}
