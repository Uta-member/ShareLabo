using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Post
{
    public interface IPostUpdateCommandService : ICommandService<IPostUpdateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required Optional<PostContent> PostContentOptional { get; init; }

            public required Optional<PostTitle> PostTitleOptional { get; init; }

            public required PostId TargetPostId { get; init; }
        }
    }
}
