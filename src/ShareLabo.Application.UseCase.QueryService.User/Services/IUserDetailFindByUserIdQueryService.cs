using CSStack.TADA;

namespace ShareLabo.Application.UseCase.QueryService.User
{
    public interface IUserDetailFindByUserIdQueryService
        : IQueryService<IUserDetailFindByUserIdQueryService.Req, IUserDetailFindByUserIdQueryService.Res>
    {
        public sealed record Req : IQueryServiceDTO
        {
            public required string UserId { get; init; }
        }

        public sealed record Res : IQueryServiceDTO
        {
            public required Optional<UserDetailReadModel> User { get; init; }
        }
    }
}
