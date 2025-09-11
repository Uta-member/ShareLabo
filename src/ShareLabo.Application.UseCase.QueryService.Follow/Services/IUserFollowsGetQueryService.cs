using CSStack.TADA;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.QueryService.Follow
{
    public interface IUserFollowsGetQueryService
        : IQueryService<IUserFollowsGetQueryService.Req, IUserFollowsGetQueryService.Res>
    {
        sealed record Req : IQueryServiceDTO
        {
            public required string UserId { get; init; }
        }

        sealed record Res : IQueryServiceDTO
        {
            public required ImmutableList<FollowReadModel> Follows { get; init; }
        }
    }
}
