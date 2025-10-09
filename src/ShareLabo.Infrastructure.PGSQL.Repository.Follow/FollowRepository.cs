using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.Repository.Follow
{
    public sealed class FollowRepository : IFollowRepository<ShareLaboPGSQLTransaction>
    {
        public async ValueTask DeleteAsync(
            ShareLaboPGSQLTransaction session,
            FollowEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            await factory.Query("follows")
                .Where("from_user_id", entity.FollowId.FollowFromId.Value)
                .Where("to_user_id", entity.FollowId.FollowToId.Value)
                .DeleteAsync(cancellationToken: cancellationToken);
        }

        public async ValueTask<ImmutableList<FollowEntity>> FindByFollowingUserIdAsync(
            ShareLaboPGSQLTransaction session,
            UserId followingUserId,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbFollows = await factory.Query("follows")
                .Where("from_user_id", followingUserId.Value)
                .GetAsync<DbFollow>(cancellationToken: cancellationToken);

            return dbFollows.Select(
                dbFollow => FollowEntity.Reconstruct(
                    new FollowEntity.ReconstructCommand()
                    {
                        FollowId =
                            FollowIdentifier.Reconstruct(
                                        UserId.Reconstruct(dbFollow.FromUserId),
                                        UserId.Reconstruct(dbFollow.ToUserId)),
                        FollowStartDateTime = dbFollow.FollowStartDateTime,
                    }))
                .ToImmutableList();
        }

        public async ValueTask<Optional<FollowEntity>> FindByIdentifierAsync(
            ShareLaboPGSQLTransaction session,
            FollowIdentifier identifier,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbFollow = await factory.Query("follows")
                .Where("from_user_id", identifier.FollowFromId.Value)
                .Where("to_user_id", identifier.FollowToId.Value)
                .FirstOrDefaultAsync<DbFollow>(cancellationToken: cancellationToken);

            if(dbFollow is null)
            {
                return Optional<FollowEntity>.Empty;
            }

            return FollowEntity.Reconstruct(
                new FollowEntity.ReconstructCommand()
                {
                    FollowId =
                        FollowIdentifier.Reconstruct(
                                UserId.Reconstruct(dbFollow.FromUserId),
                                UserId.Reconstruct(dbFollow.ToUserId)),
                    FollowStartDateTime = dbFollow.FollowStartDateTime,
                });
        }

        public async ValueTask SaveAsync(
            ShareLaboPGSQLTransaction session,
            FollowEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbFollow = await factory.Query("follows")
                .Where("from_user_id", entity.FollowId.FollowFromId.Value)
                .Where("to_user_id", entity.FollowId.FollowToId.Value)
                .FirstOrDefaultAsync<DbFollow>(cancellationToken: cancellationToken);

            if(dbFollow is null)
            {
                await factory.Query("follows")
                    .InsertAsync(
                        new DbFollow
                        {
                            FromUserId = entity.FollowId.FollowFromId.Value,
                            ToUserId = entity.FollowId.FollowToId.Value,
                            FollowStartDateTime = entity.FollowStartDateTime,
                            InsertTimeStamp = operateInfo.OperatedDateTime,
                            InsertUserId = operateInfo.Operator.Value,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }
            else
            {
                await factory.Query("follows")
                    .Where("from_user_id", entity.FollowId.FollowFromId.Value)
                    .Where("to_user_id", entity.FollowId.FollowToId.Value)
                    .UpdateAsync(
                        new DbFollow
                        {
                            FollowStartDateTime = entity.FollowStartDateTime,
                            UpdateTimeStamp = operateInfo.OperatedDateTime,
                            UpdateUserId = operateInfo.Operator.Value,
                            FromUserId = entity.FollowId.FollowFromId.Value,
                            InsertTimeStamp = dbFollow.InsertTimeStamp,
                            InsertUserId = dbFollow.InsertUserId,
                            ToUserId = entity.FollowId.FollowToId.Value,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }
        }
    }
}
