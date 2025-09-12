using Moq;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Follow.Tests
{
    public sealed class FollowCreateDomainServiceTest
    {
        private readonly FollowCreateDomainService<DummyFollowSession, DummyUserSession> _domainService;
        private readonly Mock<IFollowAggregateService<DummyFollowSession>> _followServiceMock;
        private readonly Mock<IUserAggregateService<DummyUserSession>> _userServiceMock;

        public FollowCreateDomainServiceTest()
        {
            _followServiceMock = new Mock<IFollowAggregateService<DummyFollowSession>>();
            _userServiceMock = new Mock<IUserAggregateService<DummyUserSession>>();

            _domainService = new FollowCreateDomainService<DummyFollowSession, DummyUserSession>(
                _followServiceMock.Object,
                _userServiceMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WhenBothUsersExist_CallsFollowCreate()
        {
            // Arrange
            var userAId = UserId.Reconstruct(new string('a', 8));
            var userBId = UserId.Reconstruct(new string('b', 8));

            var userAEntity = UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    AccountId = UserAccountId.Reconstruct(new string('c', 8)),
                    Id = userAId,
                    Name = UserName.Reconstruct(new string('d', 8)),
                    Status = UserEntity.StatusEnum.Enabled,
                });
            var userBEntity = UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    AccountId = UserAccountId.Reconstruct(new string('e', 8)),
                    Id = userBId,
                    Name = UserName.Reconstruct(new string('f', 8)),
                    Status = UserEntity.StatusEnum.Enabled,
                });

            var followId = new FollowIdentifier()
            {
                FollowFromId = userAId,
                FollowToId = userBId,
            };
            var req = new IFollowCreateDomainService<DummyFollowSession, DummyUserSession>.Req
            {
                FollowId = followId,
                FollowSession = new DummyFollowSession(),
                UserSession = new DummyUserSession(),
                FollowStartDateTime = DateTime.Now,
                OperateInfo =
                    new OperateInfo()
                    {
                        OperatedDateTime = DateTime.Now,
                        Operator = userAId,
                    }
            };

            // IUserAggregateServiceのFindByIdentifierAsyncがユーザーを返すようにモック
            _userServiceMock.Setup(
                x => x.GetEntityByIdentifierAsync(
                    It.IsAny<DummyUserSession>(),
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(userAEntity); // ユーザーが存在するケース

            // Act
            await _domainService.ExecuteAsync(req);

            // Assert
            // IFollowAggregateServiceのCreateAsyncが1回呼び出されたことを検証
            _followServiceMock.Verify(
                x => x.CreateAsync(
                    It.Is<IFollowAggregateService<DummyFollowSession>.CreateReq>(
                        r => r.CreateCommand.FollowId == followId),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        public class DummyFollowSession : IDisposable
        {
            public void Dispose()
            {
            }
        }

        public class DummyUserSession : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
