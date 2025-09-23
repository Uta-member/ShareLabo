using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.Post
{
    public interface IPostDeleteCommandService : ICommandService<IPostDeleteCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required OperateInfoDTO OperateInfo { get; init; }

            public required string TargetPostId { get; init; }
        }
    }
}
