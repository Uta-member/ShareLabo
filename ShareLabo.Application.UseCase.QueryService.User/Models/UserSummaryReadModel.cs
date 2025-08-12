using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Application.UseCase.QueryService.User
{
    public sealed record UserSummaryReadModel
    {
        public required UserEntity.StatusEnum Status { get; init; }

        public required string UserAccountId { get; init; }

        public required string UserId { get; init; }

        public required string UserName { get; init; }
    }
}
