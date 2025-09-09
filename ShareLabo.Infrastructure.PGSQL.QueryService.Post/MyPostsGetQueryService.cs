using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.Post
{
    public sealed class MyPostsGetQueryService : IMyPostsGetQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public MyPostsGetQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<IMyPostsGetQueryService.Res> ExecuteAsync(
            IMyPostsGetQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var query = factory
                .Query("posts as p")
                .LeftJoin("users as u", "p.post_user_id", "u.user_id");

            query = query.Where("p.post_user_id", req.UserId);

            if(req.StartPostSequenceId is not null)
            {
                query = query.Where(
                    "p.post_sequence_id",
                    req.ToBefore ? "<=" : ">=",
                    req.StartPostSequenceId.Value);
            }

            query = query
                .Select(
                    "p.post_id",
                    "p.sequence_id as post_sequence_id",
                    "p.post_user_id",
                    "p.post_title",
                    "p.post_date_time",
                    "u.user_name");

            if(req.ToBefore)
            {
                query = query.OrderByDesc("p.post_sequence_id");
            }
            else
            {
                query = query.OrderBy("p.post_sequence_id");
            }

            query = query.Limit(req.Length);

            var dbPosts = await query.GetAsync<DbPostResult>(cancellationToken: cancellationToken);

            return new IMyPostsGetQueryService.Res()
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
