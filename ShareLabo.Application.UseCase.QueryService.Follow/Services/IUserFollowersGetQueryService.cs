using CSStack.TADA;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.QueryService.Follow
{
    public interface IUserFollowersGetQueryService
        : IQueryService<IUserFollowersGetQueryService.Req, IUserFollowersGetQueryService.Res>
    {
        public sealed record Req : IQueryServiceDTO
        {
            public required string UserId { get; init; }
        }

        public sealed record Res : IQueryServiceDTO
        {
            public required ImmutableList<FollowReadModel> Followers { get; init; }
        }
    }
}
