using CSStack.TADA;
using Moq;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Follow.Tests
{
    public sealed class FollowAggregateServiceTest
    {
        private readonly FollowAggregateService<FollowDummySession> _followAggregateService;
        private readonly Mock<IFollowRepository<FollowDummySession>> _followRepositoryMock;
        private readonly FollowDummySession _followSession;

        public FollowAggregateServiceTest()
        {
            _followRepositoryMock = new Mock<IFollowRepository<FollowDummySession>>();
            _followAggregateService = new FollowAggregateService<FollowDummySession>(_followRepositoryMock.Object);
            _followSession = new FollowDummySession();
        }

        [Fact]
        public async Task CreateAsync_正常()
        {
            // Arrange
            var followId = FollowIdentifier.Create(UserId.Create(new string('a', 8)), UserId.Create(new string('b', 8)));
            var createCommand = new FollowEntity.CreateCommand()
            {
                FollowId = followId,
                FollowStartDateTime = new DateTime(2025, 9, 1),
            };
            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = UserId.Reconstruct(new string('c', 8)),
            };

            var createReq = new IFollowAggregateService<FollowDummySession>.CreateReq
            {
                CreateCommand = createCommand,
                OperateInfo = operateInfo,
                Session = _followSession
            };

            _followRepositoryMock.Setup(
                r => r.FindByIdentifierAsync(
                    _followSession,
                    followId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<FollowEntity>.Empty);

            // Act
            await _followAggregateService.CreateAsync(createReq);

            // Assert
            // SaveAsync が1回呼び出されたことを検証
            _followRepositoryMock.Verify(
                r => r.SaveAsync(
                    _followSession,
                    It.IsAny<FollowEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_同一オブジェクト存在()
        {
            // Arrange
            var followId = FollowIdentifier.Create(UserId.Create(new string('a', 8)), UserId.Create(new string('b', 8)));
            var createCommand = new FollowEntity.CreateCommand()
            {
                FollowId = followId,
                FollowStartDateTime = new DateTime(2025, 9, 1),
            };
            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = UserId.Reconstruct(new string('c', 8)),
            };

            var createReq = new IFollowAggregateService<FollowDummySession>.CreateReq
            {
                CreateCommand = createCommand,
                OperateInfo = operateInfo,
                Session = _followSession
            };

            _followRepositoryMock.Setup(
                r => r.FindByIdentifierAsync(
                    _followSession,
                    followId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    FollowEntity.Reconstruct(
                        new FollowEntity.ReconstructCommand()
                        {
                            FollowId = followId,
                            FollowStartDateTime = new DateTime(2025, 9, 1),
                        }));

            // Act
            await Assert.ThrowsAsync<ObjectAlreadyExistException>(
                async () => await _followAggregateService.CreateAsync(createReq));

            // SaveAsync が呼び出されないことを検証
            _followRepositoryMock.Verify(
                r => r.SaveAsync(
                    _followSession,
                    It.IsAny<FollowEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_正常()
        {
            // Arrange
            var followId = FollowIdentifier.Reconstruct(
                UserId.Reconstruct(new string('a', 8)),
                UserId.Reconstruct(new string('b', 8)));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = UserId.Reconstruct(new string('c', 8)),
            };

            var deleteReq = new IFollowAggregateService<FollowDummySession>.DeleteReq
            {
                FollowId = followId,
                OperateInfo = operateInfo,
                Session = _followSession
            };

            var existingEntity = FollowEntity.Reconstruct(
                new FollowEntity.ReconstructCommand()
                {
                    FollowId = followId,
                    FollowStartDateTime = new DateTime(2025, 9, 1),
                });
            _followRepositoryMock.Setup(
                r => r.FindByIdentifierAsync(
                    _followSession,
                    followId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEntity);

            // Act
            await _followAggregateService.DeleteAsync(deleteReq);

            // Assert
            _followRepositoryMock.Verify(
                r => r.DeleteAsync(
                    _followSession,
                    existingEntity,
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_存在しないオブジェクト()
        {
            // Arrange
            var followId = FollowIdentifier.Reconstruct(
                UserId.Reconstruct(new string('a', 8)),
                UserId.Reconstruct(new string('b', 8)));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = UserId.Reconstruct(new string('c', 8)),
            };

            var deleteReq = new IFollowAggregateService<FollowDummySession>.DeleteReq
            {
                FollowId = followId,
                OperateInfo = operateInfo,
                Session = _followSession
            };

            _followRepositoryMock.Setup(
                r => r.FindByIdentifierAsync(
                    _followSession,
                    followId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<FollowEntity>.Empty);

            // Act
            await Assert.ThrowsAsync<ObjectNotFoundException>(
                async () => await _followAggregateService.DeleteAsync(deleteReq));

            // Assert
            _followRepositoryMock.Verify(
                r => r.DeleteAsync(
                    _followSession,
                    It.IsAny<FollowEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        public class FollowDummySession : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
