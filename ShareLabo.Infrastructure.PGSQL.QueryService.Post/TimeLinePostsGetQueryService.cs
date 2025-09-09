using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.Post
{
    public sealed class TimeLinePostsGetQueryService : ITimeLinePostsGetQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public TimeLinePostsGetQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<ITimeLinePostsGetQueryService.Res> ExecuteAsync(
            ITimeLinePostsGetQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();

            var factory = new QueryFactory(connection, new PostgresCompiler());

            var timeLineQuery = factory.Query("time_line_filters")
                .Where("time_line_id", req.TimeLineId);

            var dbTimeLines = await timeLineQuery.GetAsync<DbTimeLineFilter>(cancellationToken: cancellationToken);

            var postQuery = factory
                .Query("posts as p")
                .LeftJoin("users as u", "p.post_user_id", "u.user_id");

            postQuery = postQuery.WhereIn("p.post_user_id", dbTimeLines.Select(x => x.UserId));

            if(req.StartPostSequenceId is not null)
            {
                postQuery = postQuery.Where(
                    "p.post_sequence_id",
                    req.ToBefore ? "<=" : ">=",
                    req.StartPostSequenceId.Value);
            }

            postQuery = postQuery
                .Select(
                    "p.post_id",
                    "p.sequence_id as post_sequence_id",
                    "p.post_user_id",
                    "p.post_title",
                    "p.post_date_time",
                    "u.user_name");

            if(req.ToBefore)
            {
                postQuery = postQuery.OrderByDesc("p.post_sequence_id");
            }
            else
            {
                postQuery = postQuery.OrderBy("p.post_sequence_id");
            }

            postQuery = postQuery.Limit(req.Length);

            var dbPosts = await postQuery.GetAsync<DbPostResult>(cancellationToken: cancellationToken);

            return new ITimeLinePostsGetQueryService.Res()
            {
                PostSummaries =
                    dbPosts.Select(
                        x => new PostSummaryReadModel()
                    {
                        PostId = x.PostId,
                        PostSequenceId = x.PostSequenceId,
                        Title = x.PostTitle,
                        PostUser =
                            new PostUserReadModel()
                                    {
                                        PostUserId = x.PostUserId,
                                        PostUserName = x.UserName,
                                    },
                        PostDateTime = x.PostDateTime,
                    })
                        .ToImmutableList(),
            };
        }

        private record DbPostResult
        {
            public required DateTime PostDateTime { get; init; }

            public required string PostId { get; init; }

            public required long PostSequenceId { get; init; }

            public required string PostTitle { get; init; }

            public required string PostUserId { get; init; }

            public required string UserName { get; init; }
        }
    }
}
