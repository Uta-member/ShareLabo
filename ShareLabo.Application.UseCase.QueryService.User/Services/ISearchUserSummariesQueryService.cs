using CSStack.TADA;
using ShareLabo.Domain.Aggregate.User;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.QueryService.User
{
    public interface ISearchUserSummariesQueryService
        : IQueryService<ISearchUserSummariesQueryService.Req, ISearchUserSummariesQueryService.Res>
    {
        public sealed record Req : IQueryServiceDTO
        {
            public Optional<string> AccountIdStrOptional { get; init; } = Optional<string>.Empty;

            public Optional<int> LimitOptional { get; init; } = Optional<int>.Empty;

            public Optional<int> StartIndexOptional { get; init; } = Optional<int>.Empty;

            public Optional<ImmutableList<UserEntity.StatusEnum>> TargetStatusesOptional
            {
                get;
                init;
            } = Optional<ImmutableList<UserEntity.StatusEnum>>.Empty;

            public Optional<string> UserIdStrOptional { get; init; } = Optional<string>.Empty;

            public Optional<string> UserNameStrOptional { get; init; } = Optional<string>.Empty;
        }

        public sealed record Res : IQueryServiceDTO
        {
            public required ImmutableList<UserSummaryReadModel> Users { get; init; }
        }
    }
}
