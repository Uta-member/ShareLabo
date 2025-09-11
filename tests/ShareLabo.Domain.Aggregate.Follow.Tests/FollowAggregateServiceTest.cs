using CSStack.TADA;
using Moq;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Follow.Tests
{
    public sealed class FollowAggregateServiceTest
    {
        private readonly Mock<IFollowRepository<DummySession>> _repositoryMock;
        private readonly FollowAggregateService<DummySession> _service;
        private readonly DummySession _session;

        public FollowAggregateServiceTest()
        {
            _repositoryMock = new Mock<IFollowRepository<DummySession>>();
            _service = new FollowAggregateService<DummySession>(_repositoryMock.Object);
            _session = new DummySession();
        }

        [Fact]
        public async Task CreateAsync_正常()
        {
            // Arrange
            var createCommand = new FollowEntity.CreateCommand()
            {
                FollowId =
                    new FollowIdentifier()
                    {
                        FollowFromId = UserId.Create(new string('a', 8)),
                        FollowToId = UserId.Create(new string('b', 8)),
                    },
                FollowStartDateTime = DateTime.Now,
            };
            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = DateTime.Now,
                Operator = UserId.Reconstruct(new string('c', 8)),
            };

            var createReq = new FollowAggregateService<DummySession>.CreateReq
            {
                CreateCommand = createCommand,
                OperateInfo = operateInfo,
                Session = _session
            };

            _repositoryMock.Setup(
                r => r.FindByIdentifierAsync(
                    It.IsAny<DummySession>(),
                    It.IsAny<FollowIdentifier>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<FollowEntity>.Empty);

            // Act
            await _service.CreateAsync(createReq);

            // Assert
            // SaveAsync が1回呼び出されたことを検証
            _repositoryMock.Verify(
                r => r.SaveAsync(
                    It.IsAny<DummySession>(),
                    It.IsAny<FollowEntity>(),
                    It.IsAny<OperateInfo>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_同一オブジェクト存在()
        {
            // Arrange
            var createCommand = new FollowEntity.CreateCommand()
            {
                FollowId =
                    new FollowIdentifier()
                    {
                        FollowFromId = UserId.Create(new string('a', 8)),
                        FollowToId = UserId.Create(new string('b', 8)),
                    },
                FollowStartDateTime = DateTime.Now,
            };
            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = DateTime.Now,
                Operator = UserId.Reconstruct(new string('c', 8)),
            };

            var createReq = new FollowAggregateService<DummySession>.CreateReq
            {
                CreateCommand = createCommand,
                OperateInfo = operateInfo,
                Session = _session
            };

            _repositoryMock.Setup(
                r => r.FindByIdentifierAsync(
                    It.IsAny<DummySession>(),
                    It.IsAny<FollowIdentifier>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    FollowEntity.Reconstruct(
                        new FollowEntity.ReconstructCommand()
                        {
                            FollowId =
                                new FollowIdentifier()
                                        {
                                            FollowFromId = UserId.Create(new string('a', 8)),
                                            FollowToId = UserId.Create(new string('b', 8)),
                                        },
                            FollowStartDateTime = DateTime.Now,
                        }));

            // Act
            await Assert.ThrowsAsync<ObjectAlreadyExistException>(async () => await _service.CreateAsync(createReq));

            // SaveAsync が呼び出されないことを検証
            _repositoryMock.Verify(
                r => r.SaveAsync(
                    It.IsAny<DummySession>(),
                    It.IsAny<FollowEntity>(),
                    It.IsAny<OperateInfo>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_正常()
        {
            // Arrange
            var followId = new FollowIdentifier()
            {
                FollowFromId = UserId.Reconstruct(new string('a', 8)),
                FollowToId = UserId.Reconstruct(new string('b', 8)),
            };
            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = DateTime.Now,
                Operator = UserId.Reconstruct(new string('c', 8)),
            };

            var deleteReq = new FollowAggregateService<DummySession>.DeleteReq
            {
                FollowId = followId,
                OperateInfo = operateInfo,
                Session = _session
            };

            var existingEntity = FollowEntity.Reconstruct(
                new FollowEntity.ReconstructCommand()
                {
                    FollowId = followId,
                    FollowStartDateTime = DateTime.Now,
                });
            _repositoryMock.Setup(
                r => r.FindByIdentifierAsync(
                    It.IsAny<DummySession>(),
                    It.IsAny<FollowIdentifier>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEntity);

            // Act
            await _service.DeleteAsync(deleteReq);

            // Assert
            // Repository.DeleteAsync が1回呼び出されたことを検証
            _repositoryMock.Verify(
                r => r.DeleteAsync(
                    It.IsAny<DummySession>(),
                    It.IsAny<FollowEntity>(),
                    It.IsAny<OperateInfo>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            // DeleteAsync に渡されたエンティティが正しいことを検証
            _repositoryMock.Verify(
                r => r.DeleteAsync(
                    _session,
                    existingEntity, // モックで返したエンティティと同じインスタンスが渡されることを検証
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_存在しないオブジェクト()
        {
            // Arrange
            var followId = new FollowIdentifier()
            {
                FollowFromId = UserId.Reconstruct(new string('a', 8)),
                FollowToId = UserId.Reconstruct(new string('b', 8)),
            };
            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = DateTime.Now,
                Operator = UserId.Reconstruct(new string('c', 8)),
            };

            var deleteReq = new FollowAggregateService<DummySession>.DeleteReq
            {
                FollowId = followId,
                OperateInfo = operateInfo,
                Session = _session
            };

            _repositoryMock.Setup(
                r => r.FindByIdentifierAsync(
                    It.IsAny<DummySession>(),
                    It.IsAny<FollowIdentifier>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<FollowEntity>.Empty);

            // Act
            await Assert.ThrowsAsync<ObjectNotFoundException>(async () => await _service.DeleteAsync(deleteReq));

            // Assert
            // Repository.DeleteAsync が1回呼び出されたことを検証
            _repositoryMock.Verify(
                r => r.DeleteAsync(
                    It.IsAny<DummySession>(),
                    It.IsAny<FollowEntity>(),
                    It.IsAny<OperateInfo>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        public class DummySession : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
