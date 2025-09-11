using ShareLabo.Application.UseCase.QueryService.TimeLine;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.TimeLine
{
    public sealed class UserTimeLinesGetQueryService : IUserTimeLinesGetQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public UserTimeLinesGetQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<IUserTimeLinesGetQueryService.Res> ExecuteAsync(
            IUserTimeLinesGetQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbTimeLines = await factory.Query("time_lines as tl")
                .Where("tl.owner_id", req.UserId)
                .GetAsync<DbTimeLine>();

            return new IUserTimeLinesGetQueryService.Res()
            {
                TimeLineSummaries =
                    dbTimeLines.Select(
                        x => new TimeLineSummaryReadModel()
                    {
                        OwnerId = x.OwnerId,
                        TimeLineId = x.TimeLineId,
                        TimeLineName = x.TimeLineName,
                    })
                        .ToImmutableList(),
            };
        }
    }
}
