using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.Post
{
    public interface IPostUpdateCommandService : ICommandService<IPostUpdateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required OperateInfoWriteModel OperateInfo { get; init; }

            public Optional<string> PostContentOptional { get; init; } = Optional<string>.Empty;

            public Optional<string> PostTitleOptional { get; init; } = Optional<string>.Empty;

            public required string TargetPostId { get; init; }
        }
    }
}
