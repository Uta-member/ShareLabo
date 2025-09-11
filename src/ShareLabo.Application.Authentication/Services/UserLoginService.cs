using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.Authentication
{
    public sealed class UserLoginService<TSession>
        where TSession : IDisposable
    {
        private readonly IUserAccountRepository<TSession> _userAccountRepository;

        public UserLoginService(IUserAccountRepository<TSession> userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async ValueTask<Res> ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            var userAccountOptional = await _userAccountRepository.FindByUserAccountIdAsync(
                req.Session,
                req.UserAccountId,
                cancellationToken);

            if(!userAccountOptional.TryGetValue(out var userAccount) ||
                userAccount.Status != UserAccountModel.StatusEnum.Enabled)
            {
                return new Res()
                {
                    IsAuthorized = false,
                    LoginResultDetail = LoginResultDetail.NotAuthorized,
                };
            }

            var userAccountWithPasswordOptional = await _userAccountRepository.FindWithPasswordHashByUserIdAsync(
                req.Session,
                userAccount.UserId,
                cancellationToken);

            if(!userAccountWithPasswordOptional.TryGetValue(out var userAccountWithPassword))
            {
                return new Res()
                {
                    IsAuthorized = false,
                    LoginResultDetail = LoginResultDetail.NotAuthorized,
                };
            }

            var passwordHash = userAccountWithPassword.Item2;
            if(!_userAccountRepository.VerifyPassword(passwordHash, req.Password))
            {
                return new Res()
                {
                    IsAuthorized = false,
                    LoginResultDetail = LoginResultDetail.NotAuthorized,
                };
            }

            return new Res()
            {
                IsAuthorized = true,
                LoginResultDetail = LoginResultDetail.Authorized,
            };
        }

        public sealed record Req
        {
            public required AccountPassword Password { get; init; }

            public required TSession Session { get; init; }

            public required UserAccountId UserAccountId { get; init; }
        }

        public sealed record Res
        {
            public required bool IsAuthorized { get; init; }

            public required LoginResultDetail LoginResultDetail { get; init; }
        }
    }
}
