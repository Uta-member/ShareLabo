using ShareLabo.Application.UseCase.QueryService.Follow;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.Follow
{
    public sealed class UserFollowsGetQueryService : IUserFollowsGetQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public UserFollowsGetQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<IUserFollowsGetQueryService.Res> ExecuteAsync(
            IUserFollowsGetQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbFollowers = await factory.Query("follows as f")
                .Join("users as u", "f.to_user_id", "u.user_id")
                .Where("f.from_user_id", req.UserId)
                .Select("f.to_user_id as UserId", "u.account_id", "u.user_name", "f.follow_start_date_time")
                .OrderByDesc("f.follow_start_date_time")
                .GetAsync<DbFollowWithUser>(cancellationToken: cancellationToken);

            return new IUserFollowsGetQueryService.Res()
            {
                Follows =
                    dbFollowers.Select(
                        x => new FollowReadModel()
                    {
                        AccountId = x.AccountId,
                        FollowStartDateTime = x.FollowStartDateTime,
                        UserId = x.UserId,
                        UserName = x.UserName,
                    })
                        .ToImmutableList(),
            };
        }

        private record DbFollowWithUser
        {
            public required string AccountId { get; init; }

            public required DateTime FollowStartDateTime { get; init; }

            public required string UserId { get; init; }

            public required string UserName { get; init; }
        }
    }
}
