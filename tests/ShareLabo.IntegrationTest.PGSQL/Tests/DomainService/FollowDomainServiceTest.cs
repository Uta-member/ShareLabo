using CSStack.TADA;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.DomainService.Follow;
using ShareLabo.Domain.DomainService.User;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.PGSQL.Toolkit;

namespace ShareLabo.IntegrationTest.PGSQL
{
    public sealed class FollowDomainServiceTest : IClassFixture<SharedServiceProviderFixture>
    {
        private readonly IServiceProvider _serviceProvider;

        public FollowDomainServiceTest(SharedServiceProviderFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public async Task 作成_削除()
        {
            var transactionService = _serviceProvider.GetRequiredService<ITransactionService<ShareLaboPGSQLTransaction>>(
                );
            using var transaction = await transactionService.BeginAsync();

            try
            {
                var userCreateDomainService = _serviceProvider.GetRequiredService<IUserCreateDomainService<ShareLaboPGSQLTransaction>>(
                    );
                var followCreateDomainService = _serviceProvider.GetRequiredService<IFollowCreateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>>(
                    );
                var followDeleteDomainService = _serviceProvider.GetRequiredService<IFollowDeleteDomainService<ShareLaboPGSQLTransaction>>(
                    );
                var followRepository = _serviceProvider.GetRequiredService<IFollowRepository<ShareLaboPGSQLTransaction>>(
                    );

                var userAId = UserId.Create(new string('a', 8));
                var userBId = UserId.Create(new string('b', 8));

                var operateInfo = new OperateInfo()
                {
                    OperatedDateTime = new DateTime(2025, 9, 1),
                    Operator = UserId.Reconstruct(new string('b', 8)),
                };

                await userCreateDomainService.ExecuteAsync(
                    new IUserCreateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        UserAccountId = UserAccountId.Create(new string('a', 8)),
                        OperateInfo = operateInfo,
                        UserId = userAId,
                        UserName = UserName.Create(new string('a', 8)),
                        UserSession = transaction,
                    });

                await userCreateDomainService.ExecuteAsync(
                    new IUserCreateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        UserAccountId = UserAccountId.Create(new string('b', 8)),
                        OperateInfo = operateInfo,
                        UserId = userBId,
                        UserName = UserName.Create(new string('b', 8)),
                        UserSession = transaction,
                    });

                var followId = FollowIdentifier.Create(userAId, userBId);
                var followStartDateTime = new DateTime(2025, 9, 1);
                await followCreateDomainService.ExecuteAsync(
                    new IFollowCreateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>.Req()
                    {
                        FollowId = followId,
                        FollowSession = transaction,
                        FollowStartDateTime = followStartDateTime,
                        OperateInfo = operateInfo,
                        UserSession = transaction,
                    });

                var followEntityOptional = await followRepository.FindByIdentifierAsync(transaction, followId);
                Assert.True(followEntityOptional.TryGetValue(out var followEntity));
                Assert.Equal(followId, followEntity.Identifier);
                Assert.Equal(followStartDateTime.ToUniversalTime(), followEntity.FollowStartDateTime.ToUniversalTime());

                await followDeleteDomainService.ExecuteAsync(
                    new IFollowDeleteDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        FollowId = followId,
                        FollowSession = transaction,
                        OperateInfo = operateInfo,
                    });
                followEntityOptional = await followRepository.FindByIdentifierAsync(transaction, followId);
                Assert.False(followEntityOptional.HasValue);
            }
            finally
            {
                await transactionService.RollbackAsync(transaction);
            }
        }
    }
}
