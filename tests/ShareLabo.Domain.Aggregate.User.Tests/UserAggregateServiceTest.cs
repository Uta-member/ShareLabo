using CSStack.TADA;
using Moq;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.User.Tests
{
    public sealed class UserAggregateServiceTest
    {
        private readonly IUserAggregateService<UserDummySession> _userAggregateService;

        private readonly Mock<IUserRepository<UserDummySession>> _userRepositoryMock;
        private readonly UserDummySession _userSession;

        public UserAggregateServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository<UserDummySession>>();
            _userAggregateService = new UserAggregateService<UserDummySession>(_userRepositoryMock.Object);
            _userSession = new UserDummySession();
        }

        [Fact]
        public async Task CreateAsync_正常()
        {
            var userId = UserId.Create(new string('a', 8));
            var userAccountId = UserAccountId.Create(new string('a', 8));
            var userName = UserName.Create(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = userId,
            };

            _userRepositoryMock.Setup(x => x.FindByIdentifierAsync(_userSession, userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<UserEntity>.Empty);
            _userRepositoryMock.Setup(
                x => x.FindByAccountIdAsync(_userSession, userAccountId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<UserEntity>.Empty);

            await _userAggregateService.CreateAsync(
                new IUserAggregateService<UserDummySession>.CreateReq()
                {
                    Command =
                        new UserEntity.CreateCommand()
                            {
                                Id = userId,
                                AccountId = userAccountId,
                                Name = userName,
                            },
                    OperateInfo = operateInfo,
                    Session = _userSession,
                },
                It.IsAny<CancellationToken>());

            _userRepositoryMock.Verify(
                x => x.SaveAsync(_userSession, It.IsAny<UserEntity>(), operateInfo, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_同一オブジェクト存在()
        {
            var userId = UserId.Create(new string('a', 8));
            var userAccountId = UserAccountId.Create(new string('a', 8));
            var userName = UserName.Create(new string('a', 8));

            var userEntity = UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    Id = userId,
                    AccountId = userAccountId,
                    Name = userName,
                    Status = UserEntity.StatusEnum.Enabled,
                });

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = userId,
            };

            _userRepositoryMock.Setup(x => x.FindByIdentifierAsync(_userSession, userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(userEntity);
            _userRepositoryMock.Setup(
                x => x.FindByAccountIdAsync(_userSession, userAccountId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<UserEntity>.Empty);

            await Assert.ThrowsAsync<ObjectAlreadyExistException>(
                async () => await _userAggregateService.CreateAsync(
                    new IUserAggregateService<UserDummySession>.CreateReq()
                {
                    Command =
                        new UserEntity.CreateCommand()
                                {
                                    Id = userId,
                                    AccountId = userAccountId,
                                    Name = userName,
                                },
                    OperateInfo = operateInfo,
                    Session = _userSession,
                },
                    It.IsAny<CancellationToken>()));

            _userRepositoryMock.Verify(
                x => x.SaveAsync(_userSession, It.IsAny<UserEntity>(), operateInfo, It.IsAny<CancellationToken>()),
                Times.Never);

            _userRepositoryMock.Setup(x => x.FindByIdentifierAsync(_userSession, userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<UserEntity>.Empty);
            _userRepositoryMock.Setup(
                x => x.FindByAccountIdAsync(_userSession, userAccountId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(userEntity);

            await Assert.ThrowsAsync<ObjectAlreadyExistException>(
                async () => await _userAggregateService.CreateAsync(
                    new IUserAggregateService<UserDummySession>.CreateReq()
                {
                    Command =
                        new UserEntity.CreateCommand()
                                {
                                    Id = userId,
                                    AccountId = userAccountId,
                                    Name = userName,
                                },
                    OperateInfo = operateInfo,
                    Session = _userSession,
                },
                    It.IsAny<CancellationToken>()));

            _userRepositoryMock.Verify(
                x => x.SaveAsync(_userSession, It.IsAny<UserEntity>(), operateInfo, It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_正常()
        {
            var userId = UserId.Create(new string('a', 8));
            var userAccountId = UserAccountId.Reconstruct(new string('a', 8));
            var userName = UserName.Reconstruct(new string('a', 8));

            var userEntity = UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    Id = userId,
                    AccountId = userAccountId,
                    Name = userName,
                    Status = UserEntity.StatusEnum.Enabled,
                });

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = userId,
            };

            _userRepositoryMock.Setup(x => x.FindByIdentifierAsync(_userSession, userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(userEntity);

            userAccountId = UserAccountId.Create(new string('b', 8));
            userName = UserName.Create(new string('b', 8));
            await _userAggregateService.UpdateAsync(
                new IUserAggregateService<UserDummySession>.UpdateReq()
                {
                    Command =
                        new UserEntity.UpdateCommand()
                            {
                                AccountIdOptional = userAccountId,
                                NameOptional = userName,
                            },
                    OperateInfo = operateInfo,
                    Session = _userSession,
                    TargetId = userId,
                });

            _userRepositoryMock.Verify(
                x => x.SaveAsync(_userSession, It.IsAny<UserEntity>(), operateInfo, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_存在しないオブジェクト()
        {
            var userId = UserId.Reconstruct(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = userId,
            };

            _userRepositoryMock.Setup(x => x.FindByIdentifierAsync(_userSession, userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<UserEntity>.Empty);

            var userAccountId = UserAccountId.Create(new string('b', 8));
            var userName = UserName.Create(new string('b', 8));
            await Assert.ThrowsAsync<ObjectNotFoundException>(
                async () => await _userAggregateService.UpdateAsync(
                    new IUserAggregateService<UserDummySession>.UpdateReq()
                {
                    Command =
                        new UserEntity.UpdateCommand()
                                {
                                    AccountIdOptional = userAccountId,
                                    NameOptional = userName,
                                },
                    OperateInfo = operateInfo,
                    Session = _userSession,
                    TargetId = userId,
                }));

            _userRepositoryMock.Verify(
                x => x.SaveAsync(_userSession, It.IsAny<UserEntity>(), operateInfo, It.IsAny<CancellationToken>()),
                Times.Never);
        }

        public sealed record UserDummySession : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
