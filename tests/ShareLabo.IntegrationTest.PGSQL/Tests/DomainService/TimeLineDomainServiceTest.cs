using CSStack.TADA;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.DomainService.TimeLine;
using ShareLabo.Domain.DomainService.User;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using System.Collections.Immutable;

namespace ShareLabo.IntegrationTest.PGSQL
{
    public sealed class TimeLineDomainServiceTest : IClassFixture<SharedServiceProviderFixture>
    {
        private readonly IServiceProvider _serviceProvider;

        public TimeLineDomainServiceTest(SharedServiceProviderFixture fixture)
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
            var timeLineCreateDomainService = _serviceProvider.GetRequiredService<ITimeLineCreateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>>(
                );
            var timeLineUpdateDomainService = _serviceProvider.GetRequiredService<ITimeLineUpdateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>>(
                );
            var timeLineDeleteDomainService = _serviceProvider.GetRequiredService<ITimeLineDeleteDomainService<ShareLaboPGSQLTransaction>>(
                );
            var timeLineRepository = _serviceProvider.GetRequiredService<ITimeLineRepository<ShareLaboPGSQLTransaction>>(
                );

            using var transaction = await transactionService.BeginAsync();

            try
            {
                var userAId = UserId.Create(new string('a', 8));
                var operateInfo = new OperateInfo()
                {
                    OperatedDateTime = new DateTime(2025, 9, 1),
                    Operator = userAId,
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

                ImmutableList<UserId> filterUserIds = [];

                for(int i = 0; i < 50; i++)
                {
                    var userId = UserId.Create($"{new string('a', 8)}-{i}");
                    filterUserIds = filterUserIds.Add(userId);

                    await userCreateDomainService.ExecuteAsync(
                        new IUserCreateDomainService<ShareLaboPGSQLTransaction>.Req()
                        {
                            UserAccountId = UserAccountId.Create($"{new string('a', 8)}-{i}"),
                            OperateInfo = operateInfo,
                            UserId = userId,
                            UserName = UserName.Create($"{new string('a', 8)}-{i}"),
                            UserSession = transaction,
                        });
                }

                var timeLineId = TimeLineId.Create(new string('a', 8));
                var timeLineName = TimeLineName.Create(new string('a', 8));

                await timeLineCreateDomainService.ExecuteAsync(
                    new ITimeLineCreateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>.Req()
                    {
                        FilterMembers = filterUserIds,
                        Id = timeLineId,
                        Name = timeLineName,
                        OperateInfo = operateInfo,
                        OwnerId = userAId,
                        TimeLineSession = transaction,
                        UserSession = transaction,
                    });

                var timeLineEntityOptional = await timeLineRepository.FindByIdentifierAsync(transaction, timeLineId);
                Assert.True(timeLineEntityOptional.TryGetValue(out var timeLineEntity));
                Assert.Equal(timeLineId, timeLineEntity.Id);
                Assert.Equal(timeLineName, timeLineEntity.Name);
                Assert.Equal(userAId, timeLineEntity.OwnerId);
                Assert.Equal(filterUserIds, timeLineEntity.FilterMembers);

                for(int i = 50; i < 100; i++)
                {
                    var userId = UserId.Create($"{new string('a', 8)}-{i}");
                    filterUserIds = filterUserIds.Add(userId);

                    await userCreateDomainService.ExecuteAsync(
                        new IUserCreateDomainService<ShareLaboPGSQLTransaction>.Req()
                        {
                            UserAccountId = UserAccountId.Create($"{new string('a', 8)}-{i}"),
                            OperateInfo = operateInfo,
                            UserId = userId,
                            UserName = UserName.Create($"{new string('a', 8)}-{i}"),
                            UserSession = transaction,
                        });
                }

                timeLineName = TimeLineName.Create(new string('b', 8));

                await timeLineUpdateDomainService.ExecuteAsync(
                    new ITimeLineUpdateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>.Req()
                    {
                        FilterMembersOptional = filterUserIds,
                        NameOptional = timeLineName,
                        OperateInfo = operateInfo,
                        TargetId = timeLineId,
                        TimeLineSession = transaction,
                        UserSession = transaction,
                    });

                timeLineEntityOptional = await timeLineRepository.FindByIdentifierAsync(transaction, timeLineId);
                Assert.True(timeLineEntityOptional.TryGetValue(out timeLineEntity));
                Assert.Equal(timeLineId, timeLineEntity.Id);
                Assert.Equal(timeLineName, timeLineEntity.Name);
                Assert.Equal(userAId, timeLineEntity.OwnerId);
                Assert.Equal(filterUserIds, timeLineEntity.FilterMembers);

                await timeLineDeleteDomainService.ExecuteAsync(
                    new ITimeLineDeleteDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        OperateInfo = operateInfo,
                        TargetId = timeLineId,
                        TimeLineSession = transaction,
                    });

                timeLineEntityOptional = await timeLineRepository.FindByIdentifierAsync(transaction, timeLineId);
                Assert.False(timeLineEntityOptional.HasValue);
            }
            finally
            {
                await transactionService.RollbackAsync(transaction);
            }
        }
    }
}
