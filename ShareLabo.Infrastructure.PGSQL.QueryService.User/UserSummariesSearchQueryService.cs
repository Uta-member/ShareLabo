using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.User
{
    public sealed class UserSummariesSearchQueryService : IUserSummariesSearchQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public UserSummariesSearchQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<IUserSummariesSearchQueryService.Res> ExecuteAsync(
            IUserSummariesSearchQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();
            var factory = new QueryFactory(connection, new PostgresCompiler());
            var query = factory.Query("users")
                .Where("is_last", true);

            if(req.UserIdStrOptional.TryGetValue(out var userIdStr))
            {
                query = query.WhereContains("user_id", userIdStr);
            }

            if(req.AccountIdStrOptional.TryGetValue(out var accountIdStr))
            {
                query = query.WhereContains("account_id", accountIdStr);
            }

            if(req.UserNameStrOptional.TryGetValue(out var userNameStr))
            {
                query = query.WhereContains("user_name", userNameStr);
            }

            if(req.TargetStatusesOptional.TryGetValue(out var targetStatuses))
            {
                var targetConditionFlgs = targetStatuses.Select(
                    x => x switch
                    {
                        UserEntity.StatusEnum.Enabled => ConditionFlgEnum.Enabled,
                        UserEntity.StatusEnum.Disabled => ConditionFlgEnum.Disabled,
                        UserEntity.StatusEnum.Deleted => ConditionFlgEnum.Deleted,
                        _ => throw new ArgumentOutOfRangeException(),
                    });
                query = query.WhereIn("condition_flg", targetConditionFlgs);
            }

            if(req.StartIndexOptional.TryGetValue(out var startIndex))
            {
                query = query.Offset(startIndex);
            }

            if(req.LimitOptional.TryGetValue(out var limit))
            {
                query = query.Limit(limit);
            }

            var dbUsers = await query.GetAsync<DbUser>(cancellationToken: cancellationToken);

            return new IUserSummariesSearchQueryService.Res()
            {
                Users =
                    dbUsers.Select(
                        x => new UserSummaryReadModel()
                    {
                        UserAccountId = x.AccountId,
                        Status =
                            x.ConditionFlg switch
                                    {
                                        ConditionFlgEnum.Enabled => UserEntity.StatusEnum.Enabled,
                                        ConditionFlgEnum.Disabled => UserEntity.StatusEnum.Disabled,
                                        ConditionFlgEnum.Deleted => UserEntity.StatusEnum.Deleted,
                                        _ => throw new ArgumentOutOfRangeException(),
                                    },
                        UserId = x.UserId,
                        UserName = x.UserName,
                    })
                        .ToImmutableList()
            };
        }
    }
}
