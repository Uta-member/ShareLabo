using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Application.UseCase.QueryService.User
{
    public sealed record UserDetailReadModel
    {
        public required DateTime InsertTimeStamp { get; init; }

        public required string InsertUserId { get; init; }

        public required string InsertUserName { get; init; }

        public required UserEntity.StatusEnum Status { get; init; }

        public DateTime? UpdateTimeStamp { get; init; }

        public string? UpdateUserId { get; init; }

        public string? UpdateUserName { get; init; }

        public required string UserAccountId { get; init; }

        public required string UserId { get; init; }

        public required string UserName { get; init; }
    }
}
