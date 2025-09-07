using CSStack.TADA;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.User
{
    public sealed class UserDetailFindByUserIdPGSQLQueryService : IUserDetailFindByUserIdQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public UserDetailFindByUserIdPGSQLQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<IUserDetailFindByUserIdQueryService.Res> ExecuteAsync(
            IUserDetailFindByUserIdQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();
            var factory = new QueryFactory(connection, new PostgresCompiler());
            var query = factory.Query("users as user_info")
                .Where("user_info.user_id", req.UserId)
                .Where("user_info.is_last", true)
                .LeftJoin("users as insert_user", "user_info.insert_user_id", "insert_user.user_id")
                .LeftJoin("users as update_user", "user_info.update_user_id", "update_user.user_id")
                .Select(
                    "user_info.{pointer_no, seq, user_id, account_id, user_name, condition_flg, is_last, insert_time_stamp, insert_user_id, update_time_stamp, update_user_id}")
                .Select("insert_user.{user_name as insert_user_name}")
                .Select("update_user.{user_name as update_user_name}");
            var dbUser = await query.FirstOrDefaultAsync<DbUserDetail>(cancellationToken: cancellationToken);
            if(dbUser == null)
            {
                return new IUserDetailFindByUserIdQueryService.Res()
                {
                    User = Optional<UserDetailReadModel>.Empty,
                };
            }

            return new IUserDetailFindByUserIdQueryService.Res()
            {
                User =
                    new UserDetailReadModel()
                    {
                        UserAccountId = dbUser.AccountId,
                        InsertTimeStamp = dbUser.InsertTimeStamp,
                        InsertUserId = dbUser.InsertUserId,
                        InsertUserName = dbUser.InsertUserName ?? string.Empty,
                        Status =
                            dbUser.ConditionFlg switch
                                {
                                    ConditionFlgEnum.Enabled => UserEntity.StatusEnum.Enabled,
                                    ConditionFlgEnum.Disabled => UserEntity.StatusEnum.Disabled,
                                    ConditionFlgEnum.Deleted => UserEntity.StatusEnum.Deleted,
                                    _ => throw new ArgumentOutOfRangeException(),
                                },
                        UpdateTimeStamp = dbUser.UpdateTimeStamp,
                        UpdateUserId = dbUser.UpdateUserId,
                        UpdateUserName = dbUser.UpdateUserName,
                        UserId = dbUser.UserId,
                        UserName = dbUser.UserName,
                    },
            };
        }

        private sealed class DbUserDetail : DbUser
        {
            public string? InsertUserName { get; init; }

            public string? UpdateUserName { get; init; }
        }
    }
}
