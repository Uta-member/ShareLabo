using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Post
{
    public interface IPostCreateCommandService : ICommandService<IPostCreateCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required PostContent PostContent { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required PostId PostId { get; init; }

            public required PostTitle PostTitle { get; init; }

            public required UserId PostUserId { get; init; }
        }
    }
}
