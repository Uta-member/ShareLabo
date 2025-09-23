using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.TimeLine
{
    public interface ITimeLineDeleteCommandService : ICommandService<ITimeLineDeleteCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required OperateInfoDTO OperateInfo { get; init; }

            public required string TargetId { get; init; }
        }
    }
}
