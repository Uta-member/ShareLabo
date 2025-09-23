using CSStack.TADA;

namespace ShareLabo.Application.UseCase.QueryService.User
{
    public interface IUserDetailFindByAccountIdQueryService
     : IQueryService<IUserDetailFindByAccountIdQueryService.Req, IUserDetailFindByAccountIdQueryService.Res>
    {
        public sealed record Req : IQueryServiceDTO
        {
            public required string AccountId { get; init; }
        }

        public sealed record Res : IQueryServiceDTO
        {
            public required Optional<UserDetailReadModel> UserOptional { get; init; }
        }
    }
}
