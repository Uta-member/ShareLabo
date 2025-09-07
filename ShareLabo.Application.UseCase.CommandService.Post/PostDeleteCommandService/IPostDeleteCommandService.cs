using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Post
{
    public interface IPostDeleteCommandService : ICommandService<IPostDeleteCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required PostId TargetPostId { get; init; }
        }
    }
}
