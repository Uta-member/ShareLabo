using CSStack.TADA;

namespace ShareLabo.Application.UseCase.QueryService.User
{
    public interface IFindUserDetailByAccountIdQueryService
     : IQueryService<IFindUserDetailByAccountIdQueryService.Req, IFindUserDetailByAccountIdQueryService.Res>
    {
        public sealed record Req : IQueryServiceDTO
        {
            public required string AccountId { get; init; }
        }

        public sealed record Res : IQueryServiceDTO
        {
            public required Optional<UserDetailReadModel> User { get; init; }
        }
    }
}
