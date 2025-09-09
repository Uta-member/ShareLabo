using CSStack.TADA;
using ShareLabo.Application.UseCase.QueryService.TimeLine;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.TimeLine
{
    public sealed class TimeLineFindByIdQueryService : ITimeLineFindByIdQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public TimeLineFindByIdQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<ITimeLineFindByIdQueryService.Res> ExecuteAsync(
            ITimeLineFindByIdQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbTimeLine = await factory.Query("time_lines as tl")
                .Join("users as u", "tl.owner_id", "u.user_id")
                .Where("tl.time_line_id", req.TimeLineId)
                .Select("tl.*", "u.user_name as owner_name")
                .FirstOrDefaultAsync<DbTimeLineWithOwnerName>();

            if(dbTimeLine is null)
            {
                return new ITimeLineFindByIdQueryService.Res()
                {
                    TimeLineOptional = Optional<TimeLineDetailReadModel>.Empty,
                };
            }

            var dbTimeLineFilters = await factory.Query("time_line_filters as tlf")
                .Join("users as u", "tlf.user_id", "u.user_id")
                .Where("tlf.time_line_id", dbTimeLine.TimeLineId)
                .Select("tlf.user_id", "u.user_name")
                .OrderBy("tlf.insert_time_stamp")
                .GetAsync<DbTimeLineFilterWithUserName>();

            return new ITimeLineFindByIdQueryService.Res()
            {
                TimeLineOptional =
                    new TimeLineDetailReadModel()
                    {
                        OwnerId = dbTimeLine.OwnerId,
                        OwnerName = dbTimeLine.OwnerName,
                        TimeLineFilters =
                            dbTimeLineFilters.Select(
                                    x => new TimeLineFilterReadModel()
                            {
                                UserId = x.UserId,
                                UserName = x.UserName,
                            })
                                    .ToImmutableList(),
                        TimeLineId = dbTimeLine.TimeLineId,
                        TimeLineName = dbTimeLine.TimeLineName,
                    },
            };
        }

        private class DbTimeLineFilterWithUserName
        {
            public required string UserId { get; set; }

            public required string UserName { get; set; }
        }

        private class DbTimeLineWithOwnerName : DbTimeLine
        {
            public required string OwnerName { get; init; }
        }
    }
}
