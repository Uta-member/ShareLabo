using CSStack.TADA;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ShareLabo.Infrastructure.PGSQL.QueryService.Post
{
    public sealed class PostDetailFindByIdPGSQLQueryService : IPostDetailFindByIdQueryService
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public PostDetailFindByIdPGSQLQueryService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<IPostDetailFindByIdQueryService.Res> ExecuteAsync(
            IPostDetailFindByIdQueryService.Req req,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.OpenConnection();

            var factory = new QueryFactory(connection, new PostgresCompiler());

            var query = factory
                .Query("posts as p")
                .LeftJoin("users as u", "p.post_user_id", "u.user_id")
                .Where("p.post_id", req.PostId);

            query = query
                .Select(
                    "p.post_id",
                    "p.sequence_id as post_sequence_id",
                    "p.post_user_id",
                    "p.post_title",
                    "p.post_date_time",
                    "u.user_name",
                    "p.insert_time_stamp",
                    "p.update_time_stamp");

            var dbPost = await query.FirstOrDefaultAsync<DbPostResult>(cancellationToken: cancellationToken);

            if(dbPost is null)
            {
                return new IPostDetailFindByIdQueryService.Res()
                {
                    PostDetailOptional = Optional<PostDetailReadModel>.Empty,
                };
            }

            return new IPostDetailFindByIdQueryService.Res()
            {
                PostDetailOptional =
                    new PostDetailReadModel()
                    {
                        Content = dbPost.PostContent,
                        PostDateTime = dbPost.PostDateTime,
                        PostId = dbPost.PostId,
                        PostUser =
                            new PostUserReadModel()
                                {
                                    PostUserId = dbPost.PostUserId,
                                    PostUserName = dbPost.UserName,
                                },
                        Title = dbPost.PostTitle,
                        UpdateTimeStamp = DateTime.UtcNow,
                        PostSequenceId = dbPost.PostSequenceId,
                    },
            };
        }

        private record DbPostResult
        {
            public required DateTime InsertTimeStamp { get; init; }

            public required string PostContent { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required string PostId { get; init; }

            public required long PostSequenceId { get; init; }

            public required string PostTitle { get; init; }

            public required string PostUserId { get; init; }

            public DateTime? UpdateTimeStamp { get; init; }

            public required string UserName { get; init; }
        }
    }
}
