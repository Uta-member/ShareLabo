using CSStack.TADA;

namespace ShareLabo.Application.UseCase.QueryService.User
{
    public interface IFindUserDetailByUserIdQueryService
        : IQueryService<IFindUserDetailByUserIdQueryService.Req, IFindUserDetailByUserIdQueryService.Res>
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
