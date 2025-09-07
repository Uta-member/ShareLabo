using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ShareLabo.Infrastructure.PGSQL.Repository.Post
{
    public sealed class PostPGSQLRepository : IPostRepository<ShareLaboPGSQLTransaction>
    {
        public async ValueTask DeleteAsync(
            ShareLaboPGSQLTransaction session,
            PostEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            await factory.Query("posts")
                .Where("post_id", entity.Id.Value)
                .DeleteAsync(cancellationToken: cancellationToken);
        }

        public async ValueTask<Optional<PostEntity>> FindByIdentifierAsync(
            ShareLaboPGSQLTransaction session,
            PostId identifier,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbPost = await factory.Query("posts")
                .Where("post_id", identifier.Value)
                .FirstOrDefaultAsync<DbPost>(cancellationToken: cancellationToken);

            if(dbPost == null)
            {
                return Optional<PostEntity>.Empty;
            }

            return PostEntity.Reconstruct(
                new PostEntity.ReconstructCommand()
                {
                    Content = PostContent.Reconstruct(dbPost.PostContent),
                    Id = PostId.Reconstruct(dbPost.PostId),
                    PostDateTime = dbPost.PostDateTime,
                    PostUser = UserId.Reconstruct(dbPost.PostUserId),
                    Title = PostTitle.Reconstruct(dbPost.PostTitle),
                    SequenceId = dbPost.SequenceId,
                });
        }

        public async ValueTask<Optional<PostEntity>> FindLatestPostAsync(
            ShareLaboPGSQLTransaction session,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbPost = await factory.Query("posts")
                .OrderBy("sequence_id desc")
                .FirstOrDefaultAsync<DbPost>(cancellationToken: cancellationToken);

            if(dbPost == null)
            {
                return Optional<PostEntity>.Empty;
            }

            return PostEntity.Reconstruct(
                new PostEntity.ReconstructCommand()
                {
                    Content = PostContent.Reconstruct(dbPost.PostContent),
                    Id = PostId.Reconstruct(dbPost.PostId),
                    PostDateTime = dbPost.PostDateTime,
                    PostUser = UserId.Reconstruct(dbPost.PostUserId),
                    Title = PostTitle.Reconstruct(dbPost.PostTitle),
                    SequenceId = dbPost.SequenceId,
                });
        }

        public async ValueTask SaveAsync(
            ShareLaboPGSQLTransaction session,
            PostEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbPost = await factory.Query("posts")
                .Where("post_id", entity.Identifier.Value)
                .FirstOrDefaultAsync<DbPost>(cancellationToken: cancellationToken);

            if(dbPost == null)
            {
                await factory.Query("posts")
                    .InsertAsync(
                        new DbPost()
                        {
                            InsertTimeStamp = operateInfo.OperatedDateTime,
                            InsertUserId = operateInfo.Operator.Value,
                            PostContent = entity.Content.Value,
                            PostDateTime = entity.PostDateTime,
                            PostId = entity.Id.Value,
                            PostTitle = entity.Title.Value,
                            PostUserId = entity.PostUser.Value,
                            SequenceId = entity.SequenceId,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }
            else
            {
                await factory.Query("posts")
                    .Where("post_id", entity.Identifier.Value)
                    .UpdateAsync(
                        new DbPost()
                        {
                            PostContent = entity.Content.Value,
                            PostDateTime = entity.PostDateTime,
                            PostTitle = entity.Title.Value,
                            PostUserId = entity.PostUser.Value,
                            UpdateTimeStamp = operateInfo.OperatedDateTime,
                            UpdateUserId = operateInfo.Operator.Value,
                            SequenceId = entity.SequenceId,
                            PostId = dbPost.PostId,
                            InsertTimeStamp = dbPost.InsertTimeStamp,
                            InsertUserId = dbPost.InsertUserId,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }
        }
    }
}
