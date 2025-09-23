using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.Post
{
    public interface IPostCreateCommandService : ICommandService<IPostCreateCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required OperateInfoDTO OperateInfo { get; init; }

            public required string PostContent { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required string PostId { get; init; }

            public required string PostTitle { get; init; }

            public required string PostUserId { get; init; }
        }
    }
}
