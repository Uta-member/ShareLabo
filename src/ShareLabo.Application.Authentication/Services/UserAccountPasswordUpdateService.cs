using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.Authentication
{
    public sealed class UserAccountPasswordUpdateService<TSession>
        where TSession : IDisposable
    {
        private readonly IUserAccountRepository<TSession> _userAccountRepository;

        public UserAccountPasswordUpdateService(IUserAccountRepository<TSession> userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async ValueTask ExecuteAsync(
            Req req,
            CancellationToken cancellationToken = default)
        {
            if(req.NewPassword == req.CurrentPassword)
            {
                throw new InvalidOperationException("現在のパスワードと更新後のパスワードが同じです");
            }

            req.NewPassword.Validate();

            var targetUserAccountOptional = await _userAccountRepository.FindWithPasswordHashByUserIdAsync(
                req.Session,
                req.TargetUserId,
                cancellationToken);

            if(!targetUserAccountOptional.TryGetValue(out var targetUserAccount))
            {
                throw new ObjectNotFoundException("対象のユーザが見つかりません");
            }

            var account = targetUserAccount.Item1;
            var currentPasswordHash = targetUserAccount.Item2;
            if(account.Status == UserAccountModel.StatusEnum.Deleted)
            {
                throw new InvalidOperationException("削除済みのユーザのパスワードは更新できません");
            }
            if(_userAccountRepository.VerifyPassword(currentPasswordHash, req.NewPassword))
            {
                throw new InvalidOperationException("現在のパスワードガ間違っています");
            }
            await _userAccountRepository.SaveAsync(
                req.Session,
                account,
                req.NewPassword,
                req.OperateInfo,
                cancellationToken);
        }

        public sealed record Req
        {
            public required AccountPassword CurrentPassword { get; init; }

            public required AccountPassword NewPassword { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required UserId TargetUserId { get; init; }
        }
    }
}
