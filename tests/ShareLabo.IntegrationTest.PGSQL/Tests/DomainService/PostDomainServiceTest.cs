using CSStack.TADA;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.DomainService.Post;
using ShareLabo.Domain.DomainService.User;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.PGSQL.Toolkit;

namespace ShareLabo.IntegrationTest.PGSQL
{
    public sealed class PostDomainServiceTest : IClassFixture<SharedServiceProviderFixture>
    {
        private readonly IServiceProvider _serviceProvider;

        public PostDomainServiceTest(SharedServiceProviderFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public async Task 作成_更新_削除()
        {
            var transactionService = _serviceProvider.GetRequiredService<ITransactionService<ShareLaboPGSQLTransaction>>(
                );
            var userCreateDomainService = _serviceProvider.GetRequiredService<IUserCreateDomainService<ShareLaboPGSQLTransaction>>(
                );
            var postCreateDomainService = _serviceProvider.GetRequiredService<IPostCreateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>>(
                );
            var postUpdateDomainService = _serviceProvider.GetRequiredService<IPostUpdateDomainService<ShareLaboPGSQLTransaction>>(
                );
            var postDeleteDomainService = _serviceProvider.GetRequiredService<IPostDeleteDomainService<ShareLaboPGSQLTransaction>>(
                );
            var postRepository = _serviceProvider.GetRequiredService<IPostRepository<ShareLaboPGSQLTransaction>>();

            using var transaction = await transactionService.BeginAsync();

            try
            {
                var userId = UserId.Create(new string('a', 8));
                var operateInfo = new OperateInfo()
                {
                    OperatedDateTime = new DateTime(2025, 9, 1),
                    Operator = userId,
                };

                await userCreateDomainService.ExecuteAsync(
                    new IUserCreateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        UserAccountId = UserAccountId.Create(new string('a', 8)),
                        OperateInfo = operateInfo,
                        UserId = userId,
                        UserName = UserName.Create(new string('a', 8)),
                        UserSession = transaction,
                    });

                var postId = PostId.Create(new string('a', 8));
                var postContent = PostContent.Create(new string('a', 8));
                var postTitle = PostTitle.Create(new string('a', 8));
                var postDateTime = new DateTime(2025, 9, 1);

                await postCreateDomainService.ExecuteAsync(
                    new IPostCreateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>.Req()
                    {
                        OperateInfo = operateInfo,
                        PostContent = postContent,
                        PostDateTime = postDateTime,
                        PostId = postId,
                        PostSession = transaction,
                        PostTitle = postTitle,
                        PostUser = userId,
                        UserSession = transaction,
                    });
                var postEntityOptional = await postRepository.FindByIdentifierAsync(transaction, postId);

                Assert.True(postEntityOptional.TryGetValue(out var postEntity));
                Assert.Equal(postId, postEntity.Id);
                Assert.Equal(postTitle, postEntity.Title);
                Assert.Equal(postContent, postEntity.Content);
                Assert.Equal(postDateTime.ToUniversalTime(), postEntity.PostDateTime.ToUniversalTime());

                var sequenceId = postEntity.SequenceId;
                postContent = PostContent.Create(new string('b', 8));
                postTitle = PostTitle.Create(new string('b', 8));

                await postUpdateDomainService.ExecuteAsync(
                    new IPostUpdateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        OperateInfo = operateInfo,
                        PostContentOptional = postContent,
                        PostSession = transaction,
                        PostTitleOptional = postTitle,
                        TargetPostId = postId,
                    });

                postEntityOptional = await postRepository.FindByIdentifierAsync(transaction, postId);

                Assert.True(postEntityOptional.TryGetValue(out postEntity));
                Assert.Equal(postId, postEntity.Id);
                Assert.Equal(postTitle, postEntity.Title);
                Assert.Equal(postContent, postEntity.Content);
                Assert.Equal(sequenceId + 1, postEntity.SequenceId);

                await postDeleteDomainService.ExecuteAsync(
                    new IPostDeleteDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        OperateInfo = operateInfo,
                        PostSession = transaction,
                        TargetPostId = postId,
                    });

                postEntityOptional = await postRepository.FindByIdentifierAsync(transaction, postId);
                Assert.False(postEntityOptional.HasValue);
            }
            finally
            {
                await transactionService.RollbackAsync(transaction);
            }
        }
    }
}
