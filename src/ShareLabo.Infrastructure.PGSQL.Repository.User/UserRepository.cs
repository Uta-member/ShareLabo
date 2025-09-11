using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ShareLabo.Infrastructure.PGSQL.Repository.User
{
    public sealed class UserRepository : IUserRepository<ShareLaboPGSQLTransaction>
    {
        public async ValueTask<Optional<UserEntity>> FindByAccountIdAsync(
            ShareLaboPGSQLTransaction session,
            UserAccountId accountId,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());
            var query = factory.Query("users")
                .Where("account_id", accountId.Value)
                .Where("is_last", true);

            var result = await query.FirstOrDefaultAsync<DbUser>(cancellationToken: cancellationToken);

            if(result == null)
            {
                return Optional<UserEntity>.Empty;
            }

            return UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    AccountId = UserAccountId.Reconstruct(result.AccountId),
                    Id = UserId.Reconstruct(result.UserId),
                    Name = UserName.Reconstruct(result.UserName),
                    Status =
                        result.ConditionFlg switch
                            {
                                ConditionFlgEnum.Enabled => UserEntity.StatusEnum.Enabled,
                                ConditionFlgEnum.Disabled => UserEntity.StatusEnum.Disabled,
                                ConditionFlgEnum.Deleted => UserEntity.StatusEnum.Deleted,
                                _ => throw new ArgumentOutOfRangeException()
                            }
                });
        }

        public async ValueTask<Optional<UserEntity>> FindByIdentifierAsync(
            ShareLaboPGSQLTransaction session,
            UserId identifier,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;

            var factory = new QueryFactory(connection, new PostgresCompiler());

            var query = factory.Query("users")
                .Where("user_id", identifier.Value)
                .Where("is_last", true);

            var result = await query.FirstOrDefaultAsync<DbUser>(cancellationToken: cancellationToken);

            if(result == null)
            {
                return Optional<UserEntity>.Empty;
            }

            return UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    AccountId = UserAccountId.Reconstruct(result.AccountId),
                    Id = UserId.Reconstruct(result.UserId),
                    Name = UserName.Reconstruct(result.UserName),
                    Status =
                        result.ConditionFlg switch
                            {
                                ConditionFlgEnum.Enabled => UserEntity.StatusEnum.Enabled,
                                ConditionFlgEnum.Disabled => UserEntity.StatusEnum.Disabled,
                                ConditionFlgEnum.Deleted => UserEntity.StatusEnum.Deleted,
                                _ => throw new ArgumentOutOfRangeException()
                            }
                });
        }

        public async ValueTask SaveAsync(
            ShareLaboPGSQLTransaction session,
            UserEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;

            var factory = new QueryFactory(connection, new PostgresCompiler());

            var query = factory.Query("users")
                .Where("user_id", entity.Identifier.Value)
                .Where("is_last", true);

            var result = await query.FirstOrDefaultAsync<DbUser>(cancellationToken: cancellationToken);

            if(result == null)
            {
                var dbUser = new DbUser
                {
                    AccountId = entity.AccountId.Value,
                    ConditionFlg =
                        entity.Status switch
                        {
                            UserEntity.StatusEnum.Enabled => ConditionFlgEnum.Enabled,
                            UserEntity.StatusEnum.Disabled => ConditionFlgEnum.Disabled,
                            UserEntity.StatusEnum.Deleted => ConditionFlgEnum.Deleted,
                            _ => throw new ArgumentOutOfRangeException()
                        },
                    InsertTimeStamp = operateInfo.OperatedDateTime,
                    InsertUserId = operateInfo.Operator.Value,
                    IsLast = true,
                    Seq = VersionedTableBase.InitialSeq,
                    UserId = entity.Id.Value,
                    UserName = entity.Name.Value,
                };
                await factory.Query("users")
                    .InsertAsync(dbUser.ToSnakeCaseDictionary(), cancellationToken: cancellationToken);
            }
            else
            {
                await factory.Query("users")
                    .Where("user_id", entity.Identifier.Value)
                    .Where("is_last", true)
                    .UpdateAsync(
                        new
                        {
                            IsLast = false,
                            UpdateTimeStamp = operateInfo.OperatedDateTime,
                            UpdateUserId = operateInfo.Operator.Value
                        }.ToSnakeCaseDictionary());

                var dbUser = new DbUser()
                {
                    AccountId = entity.AccountId.Value,
                    ConditionFlg =
                        entity.Status switch
                        {
                            UserEntity.StatusEnum.Enabled => ConditionFlgEnum.Enabled,
                            UserEntity.StatusEnum.Disabled => ConditionFlgEnum.Disabled,
                            UserEntity.StatusEnum.Deleted => ConditionFlgEnum.Deleted,
                            _ => throw new ArgumentOutOfRangeException()
                        },
                    InsertTimeStamp = operateInfo.OperatedDateTime,
                    InsertUserId = operateInfo.Operator.Value,
                    IsLast = true,
                    Seq = result.Seq + 1,
                    UserId = entity.Id.Value,
                    UserName = entity.Name.Value,
                };
                await factory.Query("users")
                    .InsertAsync(dbUser.ToSnakeCaseDictionary(), cancellationToken: cancellationToken);
            }
        }
    }
}
