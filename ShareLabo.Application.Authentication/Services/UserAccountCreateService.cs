using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.Authentication
{
    public sealed class UserAccountCreateService<TSession>
        where TSession : IDisposable
    {
        private readonly IUserAccountRepository<TSession> _userAccountRepository;

        public UserAccountCreateService(IUserAccountRepository<TSession> userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            var model = new UserAccountModel()
            {
                UserAccountId = req.UserAccountId,
                Status = UserAccountModel.StatusEnum.Enabled,
                UserId = req.UserId,
            };

            var targetModelOptional = await _userAccountRepository.FindByUserIdAsync(
                req.Session,
                model.UserId,
                cancellationToken);
            if(targetModelOptional.HasValue)
            {
                throw new ObjectAlreadyExistException();
            }

            targetModelOptional = await _userAccountRepository.FindByUserAccountIdAsync(
                req.Session,
                model.UserAccountId,
                cancellationToken);
            if(targetModelOptional.HasValue)
            {
                throw new ObjectAlreadyExistException();
            }

            req.AccountPassword.Validate();

            await _userAccountRepository.CreateAsync(
                req.Session,
                model,
                req.AccountPassword,
                req.OperateInfo,
                cancellationToken);
        }

        public sealed record Req
        {
            public required AccountPassword AccountPassword { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required UserAccountId UserAccountId { get; init; }

            public required UserId UserId { get; init; }
        }
    }
}
