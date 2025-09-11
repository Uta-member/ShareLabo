using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ShareLabo.Infrastructure.PGSQL.Application.Authentication
{
    public sealed class UserAccountPGSQLRepository : IUserAccountRepository<ShareLaboPGSQLTransaction>
    {
        public async ValueTask CreateAsync(
            ShareLaboPGSQLTransaction session,
            UserAccountModel userAccount,
            AccountPassword accountPassword,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            await factory.Query("accounts")
                .InsertAsync(
                    new DbAccount()
                    {
                        AccountId = userAccount.UserAccountId.Value,
                        ConditionFlg =
                            userAccount.Status switch
                                {
                                    UserAccountModel.StatusEnum.Enabled => ConditionFlgEnum.Enabled,
                                    UserAccountModel.StatusEnum.Disabled => ConditionFlgEnum.Disabled,
                                    UserAccountModel.StatusEnum.Deleted => ConditionFlgEnum.Deleted,
                                    _ => throw new ArgumentOutOfRangeException(),
                                },
                        InsertTimeStamp = operateInfo.OperatedDateTime,
                        InsertUserId = operateInfo.Operator.Value,
                        IsLast = true,
                        Seq = VersionedTableBase.InitialSeq,
                        UserId = userAccount.UserId.Value,
                        PasswordHash = accountPassword.GetHashStr(),
                    }.ToSnakeCaseDictionary());
        }

        public async ValueTask<Optional<UserAccountModel>> FindByUserAccountIdAsync(
            ShareLaboPGSQLTransaction session,
            UserAccountId userAccountId,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbAccount = await factory.Query("accounts")
                .Where("account_id", userAccountId.Value)
                .Where("is_last", true)
                .FirstOrDefaultAsync<DbAccount>(cancellationToken: cancellationToken);

            if(dbAccount == null)
            {
                return Optional<UserAccountModel>.Empty;
            }

            return new UserAccountModel()
            {
                UserAccountId = UserAccountId.Reconstruct(dbAccount.AccountId),
                Status =
                    dbAccount.ConditionFlg switch
                    {
                        ConditionFlgEnum.Enabled => UserAccountModel.StatusEnum.Enabled,
                        ConditionFlgEnum.Disabled => UserAccountModel.StatusEnum.Disabled,
                        ConditionFlgEnum.Deleted => UserAccountModel.StatusEnum.Deleted,
                        _ => throw new ArgumentOutOfRangeException(),
                    },
                UserId = UserId.Reconstruct(dbAccount.UserId),
            };
        }

        public async ValueTask<Optional<UserAccountModel>> FindByUserIdAsync(
            ShareLaboPGSQLTransaction session,
            UserId userId,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbAccount = await factory.Query("accounts")
                .Where("user_id", userId.Value)
                .Where("is_last", true)
                .FirstOrDefaultAsync<DbAccount>(cancellationToken: cancellationToken);

            if(dbAccount == null)
            {
                return Optional<UserAccountModel>.Empty;
            }

            return new UserAccountModel()
            {
                UserAccountId = UserAccountId.Reconstruct(dbAccount.AccountId),
                Status =
                    dbAccount.ConditionFlg switch
                    {
                        ConditionFlgEnum.Enabled => UserAccountModel.StatusEnum.Enabled,
                        ConditionFlgEnum.Disabled => UserAccountModel.StatusEnum.Disabled,
                        ConditionFlgEnum.Deleted => UserAccountModel.StatusEnum.Deleted,
                        _ => throw new ArgumentOutOfRangeException(),
                    },
                UserId = UserId.Reconstruct(dbAccount.UserId),
            };
        }

        public async ValueTask<Optional<(UserAccountModel, string)>> FindWithPasswordHashByUserIdAsync(
            ShareLaboPGSQLTransaction session,
            UserId userId,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbAccount = await factory.Query("accounts")
                .Where("user_id", userId.Value)
                .Where("is_last", true)
                .FirstOrDefaultAsync<DbAccount>(cancellationToken: cancellationToken);

            if(dbAccount == null)
            {
                return Optional<(UserAccountModel, string)>.Empty;
            }

            return (new UserAccountModel()
                {
                    UserAccountId = UserAccountId.Reconstruct(dbAccount.AccountId),
                    Status =
                        dbAccount.ConditionFlg switch
                        {
                            ConditionFlgEnum.Enabled => UserAccountModel.StatusEnum.Enabled,
                            ConditionFlgEnum.Disabled => UserAccountModel.StatusEnum.Disabled,
                            ConditionFlgEnum.Deleted => UserAccountModel.StatusEnum.Deleted,
                            _ => throw new ArgumentOutOfRangeException(),
                        },
                    UserId = UserId.Reconstruct(dbAccount.UserId),
                }, dbAccount.PasswordHash);
        }


        public async ValueTask SaveAsync(
            ShareLaboPGSQLTransaction session,
            UserAccountModel userAccount,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbAccount = await factory.Query("accounts")
                .Where("user_id", userAccount.UserId.Value)
                .Where("is_last", true)
                .FirstOrDefaultAsync<DbAccount>(cancellationToken: cancellationToken);

            if(dbAccount == null)
            {
                throw new ObjectNotFoundException();
            }

            await factory.Query("accounts")
                .Where("user_id", userAccount.UserId.Value)
                .Where("is_last", true)
                .UpdateAsync(
                    new
                    {
                        IsLast = false,
                        UpdateTimeStamp = operateInfo.OperatedDateTime,
                        UpdateUserId = operateInfo.Operator.Value,
                    }.ToSnakeCaseDictionary());

            await factory.Query("accounts")
                .InsertAsync(
                    new DbAccount()
                    {
                        AccountId = userAccount.UserAccountId.Value,
                        ConditionFlg =
                            userAccount.Status switch
                                {
                                    UserAccountModel.StatusEnum.Enabled => ConditionFlgEnum.Enabled,
                                    UserAccountModel.StatusEnum.Disabled => ConditionFlgEnum.Disabled,
                                    UserAccountModel.StatusEnum.Deleted => ConditionFlgEnum.Deleted,
                                    _ => throw new ArgumentOutOfRangeException(),
                                },
                        InsertTimeStamp = operateInfo.OperatedDateTime,
                        InsertUserId = operateInfo.Operator.Value,
                        IsLast = true,
                        Seq = dbAccount.Seq + 1,
                        UserId = userAccount.UserId.Value,
                        PasswordHash = dbAccount.PasswordHash,
                    }.ToSnakeCaseDictionary());
        }

        public async ValueTask SaveAsync(
            ShareLaboPGSQLTransaction session,
            UserAccountModel userAccount,
            AccountPassword accountPassword,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbAccount = await factory.Query("accounts")
                .Where("user_id", userAccount.UserId.Value)
                .Where("is_last", true)
                .FirstOrDefaultAsync<DbAccount>(cancellationToken: cancellationToken);

            if(dbAccount == null)
            {
                throw new ObjectNotFoundException();
            }

            await factory.Query("accounts")
                .Where("user_id", userAccount.UserId.Value)
                .Where("is_last", true)
                .UpdateAsync(
                    new
                    {
                        IsLast = false,
                        UpdateTimeStamp = operateInfo.OperatedDateTime,
                        UpdateUserId = operateInfo.Operator.Value,
                    }.ToSnakeCaseDictionary());

            await factory.Query("accounts")
                .InsertAsync(
                    new DbAccount()
                    {
                        AccountId = userAccount.UserAccountId.Value,
                        ConditionFlg =
                            userAccount.Status switch
                                {
                                    UserAccountModel.StatusEnum.Enabled => ConditionFlgEnum.Enabled,
                                    UserAccountModel.StatusEnum.Disabled => ConditionFlgEnum.Disabled,
                                    UserAccountModel.StatusEnum.Deleted => ConditionFlgEnum.Deleted,
                                    _ => throw new ArgumentOutOfRangeException(),
                                },
                        InsertTimeStamp = operateInfo.OperatedDateTime,
                        InsertUserId = operateInfo.Operator.Value,
                        IsLast = true,
                        Seq = dbAccount.Seq + 1,
                        UserId = userAccount.UserId.Value,
                        PasswordHash = accountPassword.Value,
                    }.ToSnakeCaseDictionary());
        }

        public bool VerifyPassword(string passwordHash, AccountPassword accountPassword)
        {
            return accountPassword.VerifyPassword(passwordHash);
        }
    }
}
