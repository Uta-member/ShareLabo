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
            await factory.Query("post_publications")
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
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }
            else
            {
                await factory.Query("posts")
                    .Where("post_id", entity.Identifier.Value)
                    .UpdateAsync(
                        new
                        {
                            PostContent = entity.Content.Value,
                            PostDateTime = entity.PostDateTime,
                            PostTitle = entity.Title.Value,
                            PostUserId = entity.PostUser.Value,
                            UpdateTimeStamp = operateInfo.OperatedDateTime,
                            UpdateUserId = operateInfo.Operator.Value,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }
        }
    }
}
