using CSStack.TADA;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.DomainService.Follow;
using ShareLabo.Domain.DomainService.TimeLine;
using ShareLabo.Domain.DomainService.User;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using System.Collections.Immutable;

namespace ShareLabo.IntegrationTest.PGSQL
{
    public sealed class UserDomainServiceTest : IClassFixture<SharedServiceProviderFixture>
    {
        private readonly IServiceProvider _serviceProvider;

        public UserDomainServiceTest(SharedServiceProviderFixture fixture)
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
            var userUpdateDomainService = _serviceProvider.GetRequiredService<IUserUpdateDomainService<ShareLaboPGSQLTransaction>>(
                );
            var userDeleteDomainService = _serviceProvider.GetRequiredService<IUserDeleteDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>>(
                );
            var userStatusUpdateDomainService = _serviceProvider.GetRequiredService<IUserStatusUpdateDomainService<ShareLaboPGSQLTransaction>>(
                );
            var timeLineCreateDomainService = _serviceProvider.GetRequiredService<ITimeLineCreateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>>(
                );
            var followCreateDomainService = _serviceProvider.GetRequiredService<IFollowCreateDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>>(
                );
            var followRepository = _serviceProvider.GetRequiredService<IFollowRepository<ShareLaboPGSQLTransaction>>(
                );
            var userRepository = _serviceProvider.GetRequiredService<IUserRepository<ShareLaboPGSQLTransaction>>();
            var timeLineRepository = _serviceProvider.GetRequiredService<ITimeLineRepository<ShareLaboPGSQLTransaction>>(
                );

            using var transaction = await transactionService.BeginAsync();

            try
            {
                var userId = UserId.Create(new string('a', 8));
                var operateInfo = new OperateInfo()
                {
                    OperatedDateTime = new DateTime(2025, 9, 1),
                    Operator = userId,
                };

                var userAccountId = UserAccountId.Create(new string('a', 8));
                var userName = UserName.Create(new string('a', 8));

                await userCreateDomainService.ExecuteAsync(
                    new IUserCreateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        UserAccountId = userAccountId,
                        OperateInfo = operateInfo,
                        UserId = userId,
                        UserName = userName,
                        UserSession = transaction,
                    });

                var userEntityOptional = await userRepository.FindByIdentifierAsync(transaction, userId);
                Assert.True(userEntityOptional.TryGetValue(out var userEntity));
                Assert.Equal(userId, userEntity.Id);
                Assert.Equal(userAccountId, userEntity.AccountId);
                Assert.Equal(userName, userEntity.Name);
                Assert.Equal(UserEntity.StatusEnum.Enabled, userEntity.Status);

                userEntityOptional = await userRepository.FindByAccountIdAsync(transaction, userAccountId);
                Assert.True(userEntityOptional.TryGetValue(out userEntity));
                Assert.Equal(userId, userEntity.Id);
                Assert.Equal(userAccountId, userEntity.AccountId);
                Assert.Equal(userName, userEntity.Name);
                Assert.Equal(UserEntity.StatusEnum.Enabled, userEntity.Status);

                userAccountId = UserAccountId.Create(new string('b', 8));
                userName = UserName.Create(new string('b', 8));

                await userUpdateDomainService.ExecuteAsync(
                    new IUserUpdateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        UserAccountIdOptional = userAccountId,
                        OperateInfo = operateInfo,
                        TargetId = userId,
                        UserNameOptional = userName,
                        UserSession = transaction,
                    });
                userEntityOptional = await userRepository.FindByIdentifierAsync(transaction, userId);
                Assert.True(userEntityOptional.TryGetValue(out userEntity));
                Assert.Equal(userId, userEntity.Id);
                Assert.Equal(userAccountId, userEntity.AccountId);
                Assert.Equal(userName, userEntity.Name);
                Assert.Equal(UserEntity.StatusEnum.Enabled, userEntity.Status);

                await userStatusUpdateDomainService.ExecuteAsync(
                    new IUserStatusUpdateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        OperateInfo = operateInfo,
                        TargetUserId = userId,
                        ToEnabled = false,
                        UserSession = transaction,
                    });
                userEntityOptional = await userRepository.FindByIdentifierAsync(transaction, userId);
                Assert.True(userEntityOptional.TryGetValue(out userEntity));
                Assert.Equal(userId, userEntity.Id);
                Assert.Equal(userAccountId, userEntity.AccountId);
                Assert.Equal(userName, userEntity.Name);
                Assert.Equal(UserEntity.StatusEnum.Disabled, userEntity.Status);

                await userStatusUpdateDomainService.ExecuteAsync(
                    new IUserStatusUpdateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        OperateInfo = operateInfo,
                        TargetUserId = userId,
                        ToEnabled = true,
                        UserSession = transaction,
                    });
                userEntityOptional = await userRepository.FindByIdentifierAsync(transaction, userId);
                Assert.True(userEntityOptional.TryGetValue(out userEntity));
                Assert.Equal(userId, userEntity.Id);
                Assert.Equal(userAccountId, userEntity.AccountId);
                Assert.Equal(userName, userEntity.Name);
                Assert.Equal(UserEntity.StatusEnum.Enabled, userEntity.Status);

                // TimeLineの準備
                ImmutableList<UserId> filterUserIds = [];
                for(int i = 0; i < 50; i++)
                {
                    var filterUserId = UserId.Create($"{new string('a', 8)}-{i}");
                    filterUserIds = filterUserIds.Add(filterUserId);

                    await userCreateDomainService.ExecuteAsync(
                        new IUserCreateDomainService<ShareLaboPGSQLTransaction>.Req()
                        {
                            UserAccountId = UserAccountId.Create($"{new string('a', 8)}-{i}"),
                            OperateInfo = operateInfo,
                            UserId = filterUserId,
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
                        OwnerId = userId,
                        TimeLineSession = transaction,
                        UserSession = transaction,
                    });
                var timeLineEntityOptional = await timeLineRepository.FindByIdentifierAsync(transaction, timeLineId);
                Assert.True(timeLineEntityOptional.TryGetValue(out var timeLineEntity));


                var userBId = UserId.Create(new string('b', 8));
                await userCreateDomainService.ExecuteAsync(
                    new IUserCreateDomainService<ShareLaboPGSQLTransaction>.Req()
                    {
                        UserAccountId = UserAccountId.Create(new string('c', 8)),
                        OperateInfo = operateInfo,
                        UserId = userBId,
                        UserName = UserName.Create(new string('b', 8)),
                        UserSession = transaction,
                    });

                var followId = FollowIdentifier.Create(userId, userBId);
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

                await userDeleteDomainService.ExecuteAsync(
                    new IUserDeleteDomainService<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>.Req(
                        )
                    {
                        FollowSession = transaction,
                        OperateInfo = operateInfo,
                        TargetId = userId,
                        TimeLineSession = transaction,
                        UserSession = transaction,
                    });

                userEntityOptional = await userRepository.FindByIdentifierAsync(transaction, userId);
                Assert.True(userEntityOptional.TryGetValue(out userEntity));
                Assert.Equal(userId, userEntity.Id);
                Assert.Equal(userAccountId, userEntity.AccountId);
                Assert.Equal(userName, userEntity.Name);
                Assert.Equal(UserEntity.StatusEnum.Deleted, userEntity.Status);

                timeLineEntityOptional = await timeLineRepository.FindByIdentifierAsync(transaction, timeLineId);
                Assert.False(timeLineEntityOptional.HasValue);
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
