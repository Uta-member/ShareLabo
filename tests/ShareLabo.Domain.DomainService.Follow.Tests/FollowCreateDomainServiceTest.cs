using CSStack.TADA;
using Moq;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Follow.Tests
{
    public sealed class FollowCreateDomainServiceTest
    {
        private readonly Mock<IFollowAggregateService<DummyFollowSession>> _followAggregateServiceMock;
        private readonly FollowCreateDomainService<DummyFollowSession, DummyUserSession> _followCreateDomainService;
        private readonly Mock<IUserAggregateService<DummyUserSession>> _userAggregateServiceMock;

        public FollowCreateDomainServiceTest()
        {
            _followAggregateServiceMock = new Mock<IFollowAggregateService<DummyFollowSession>>();
            _userAggregateServiceMock = new Mock<IUserAggregateService<DummyUserSession>>();

            _followCreateDomainService = new FollowCreateDomainService<DummyFollowSession, DummyUserSession>(
                _followAggregateServiceMock.Object,
                _userAggregateServiceMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_正常()
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
                FollowStartDateTime = new DateTime(2025, 9, 1),
                OperateInfo =
                    new OperateInfo()
                    {
                        OperatedDateTime = new DateTime(2025, 9, 1),
                        Operator = userAId,
                    }
            };

            _userAggregateServiceMock.Setup(
                x => x.GetEntityByIdentifierAsync(
                    It.IsAny<DummyUserSession>(),
                    userBId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(userBEntity);

            _userAggregateServiceMock.Setup(
                x => x.GetEntityByIdentifierAsync(
                    It.IsAny<DummyUserSession>(),
                    userAId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(userAEntity);

            // Act
            await _followCreateDomainService.ExecuteAsync(req);

            // Assert
            // IFollowAggregateServiceのCreateAsyncが1回呼び出されたことを検証
            _followAggregateServiceMock.Verify(
                x => x.CreateAsync(
                    It.Is<IFollowAggregateService<DummyFollowSession>.CreateReq>(
                        r => r.CreateCommand.FollowId == followId),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_存在しないFromユーザ()
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
                FollowStartDateTime = new DateTime(2025, 9, 1),
                OperateInfo =
                    new OperateInfo()
                    {
                        OperatedDateTime = new DateTime(2025, 9, 1),
                        Operator = userAId,
                    }
            };

            _userAggregateServiceMock.Setup(
                x => x.GetEntityByIdentifierAsync(
                    It.IsAny<DummyUserSession>(),
                    userAId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<UserEntity>.Empty);

            _userAggregateServiceMock.Setup(
                x => x.GetEntityByIdentifierAsync(
                    It.IsAny<DummyUserSession>(),
                    userBId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(userBEntity);

            // Act
            await Assert.ThrowsAsync<ObjectNotFoundException>(
                async () => await _followCreateDomainService.ExecuteAsync(req));

            // Assert
            _followAggregateServiceMock.Verify(
                x => x.CreateAsync(
                    It.Is<IFollowAggregateService<DummyFollowSession>.CreateReq>(
                        r => r.CreateCommand.FollowId == followId),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_存在しないToユーザ()
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
                FollowStartDateTime = new DateTime(2025, 9, 1),
                OperateInfo =
                    new OperateInfo()
                    {
                        OperatedDateTime = new DateTime(2025, 9, 1),
                        Operator = userAId,
                    }
            };

            _userAggregateServiceMock.Setup(
                x => x.GetEntityByIdentifierAsync(
                    It.IsAny<DummyUserSession>(),
                    userAId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(userAEntity);

            _userAggregateServiceMock.Setup(
                x => x.GetEntityByIdentifierAsync(
                    It.IsAny<DummyUserSession>(),
                    userBId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<UserEntity>.Empty);

            // Act
            await Assert.ThrowsAsync<ObjectNotFoundException>(
                async () => await _followCreateDomainService.ExecuteAsync(req));

            // Assert
            _followAggregateServiceMock.Verify(
                x => x.CreateAsync(
                    It.Is<IFollowAggregateService<DummyFollowSession>.CreateReq>(
                        r => r.CreateCommand.FollowId == followId),
                    It.IsAny<CancellationToken>()),
                Times.Never);
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
