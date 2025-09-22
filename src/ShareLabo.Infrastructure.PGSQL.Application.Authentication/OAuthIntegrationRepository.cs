using CSStack.TADA;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.Application.Authentication
{
    public sealed class OAuthIntegrationRepository : IOAuthIntegrationRepository<ShareLaboPGSQLTransaction>
    {
        public async ValueTask DeleteAsync(
            ShareLaboPGSQLTransaction session,
            OAuthIntegrationEntity entity,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            await factory.Query("o_auth_integrations")
                .Where("user_id", entity.UserId)
                .DeleteAsync(cancellationToken: cancellationToken);
        }

        public async ValueTask<Optional<OAuthIntegrationEntity>> FindByUserIdAsync(
            ShareLaboPGSQLTransaction session,
            string userId,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var oauthFindQuery = factory.Query("o_auth_integrations")
                .Where("user_id", userId)
                .OrderBy("insert_time_stamp");

            var dbOAuths = await oauthFindQuery.GetAsync<DbOAuthIntegration>(cancellationToken: cancellationToken);

            if(!dbOAuths.Any())
            {
                return Optional<OAuthIntegrationEntity>.Empty;
            }

            return new OAuthIntegrationEntity(
                userId,
                dbOAuths.Select(
                    x => new OAuthInfo()
                    {
                        OAuthIdentifier = x.OAuthIdentifier,
                        OAuthType = x.OAuthType,
                    })
                    .ToImmutableList());
        }

        public async ValueTask SaveAsync(
            ShareLaboPGSQLTransaction session,
            OAuthIntegrationEntity entity,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var oauthFindQuery = factory.Query("o_auth_integrations")
                .Where("user_id", entity.UserId)
                .OrderBy("insert_time_stamp");

            var dbOAuths = await oauthFindQuery.GetAsync<DbOAuthIntegration>(cancellationToken: cancellationToken);

            foreach(var oauthInfo in entity.OAuthInfos)
            {
                if(dbOAuths.Any(x => oauthInfo.OAuthType == x.OAuthType))
                {
                    await factory.Query("o_auth_integrations")
                        .Where("user_id", entity.UserId)
                        .Where("o_auth_type", oauthInfo.OAuthType)
                        .UpdateAsync(
                            new
                            {
                                OAuthIdentifier = oauthInfo.OAuthIdentifier,
                                UpdateTimeStamp = DateTime.Now,
                                UpdateUserId = entity.UserId,
                            }.ToSnakeCaseDictionary(),
                            cancellationToken: cancellationToken);
                }
                else
                {
                    await factory.Query("o_auth_integrations")
                        .InsertAsync(
                            new DbOAuthIntegration()
                            {
                                OAuthIdentifier = oauthInfo.OAuthIdentifier,
                                OAuthType = oauthInfo.OAuthType,
                                InsertTimeStamp = DateTime.Now,
                                InsertUserId = entity.UserId,
                                UserId = entity.UserId,
                            }.ToSnakeCaseDictionary(),
                            cancellationToken: cancellationToken);
                }
            }

            var dbToDeleteTargets = dbOAuths.Where(x => !entity.OAuthInfos.Any(y => x.OAuthType == y.OAuthType));
            await factory.Query("o_auth_integrations")
                .WhereIn("pointer_no", dbToDeleteTargets.Select(x => x.PointerNo))
                .DeleteAsync(cancellationToken: cancellationToken);
        }
    }
}
