using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.Authentication
{
    public sealed class UserAccountDeleteService<TSession>
        where TSession : IDisposable
    {
        private readonly IUserAccountRepository<TSession> _userAccountRepository;

        public UserAccountDeleteService(IUserAccountRepository<TSession> userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            var targetModelOptional = await _userAccountRepository.FindByUserIdAsync(
                req.Session,
                req.TargetId,
                cancellationToken);
            if(!targetModelOptional.TryGetValue(out var userAccount))
            {
                throw new ObjectNotFoundException();
            }
            await _userAccountRepository.SaveAsync(
                req.Session,
                new UserAccountModel()
                {
                    UserAccountId = userAccount.UserAccountId,
                    Status = UserAccountModel.StatusEnum.Deleted,
                    UserId = userAccount.UserId,
                },
                req.OperateInfo,
                cancellationToken);
        }

        public sealed record Req
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required UserId TargetId { get; init; }
        }
    }
}
