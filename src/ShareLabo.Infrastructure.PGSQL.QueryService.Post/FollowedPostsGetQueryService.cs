using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.Post
{
    public sealed class FollowedPostsGetQueryService : IFollowedPostsGetQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public FollowedPostsGetQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<IFollowedPostsGetQueryService.Res> ExecuteAsync(
            IFollowedPostsGetQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var followQuery = factory.Query("follows as f")
                .Where("f.from_user_id", req.UserId);
            var dbFollows = await followQuery.GetAsync<DbFollow>();

            var postQuery = factory.Query("posts as p")
                .LeftJoin("user as u", "p.post_user_id", "u.user_id")
                .WhereIn("p.post_user_id", dbFollows.Select(x => x.ToUserId));

            if(req.StartPostSequenceId is not null)
            {
                postQuery = postQuery.Where(
                    "p.post_sequence_id",
                    req.ToBefore ? "<=" : ">=",
                    req.StartPostSequenceId.Value);
            }

            var dbPosts = await postQuery
                .Select(
                    "p.post_id",
                    "p.sequence_id as post_sequence_id",
                    "p.post_user_id",
                    "p.post_title",
                    "p.post_date_time",
                    "u.user_name")
                .OrderByDesc("p.post_sequence_id")
                .Limit(req.Length)
                .GetAsync<DbPostResult>(cancellationToken: cancellationToken);

            return new IFollowedPostsGetQueryService.Res()
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
