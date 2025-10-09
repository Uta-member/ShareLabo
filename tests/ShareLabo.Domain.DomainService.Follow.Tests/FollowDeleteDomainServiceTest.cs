using Moq;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Follow.Tests
{
    public sealed class FollowDeleteDomainServiceTest
    {
        private readonly Mock<IFollowAggregateService<DummyFollowSession>> _followAggregateServiceMock;
        private readonly FollowDeleteDomainService<DummyFollowSession> _followDeleteDomainService;

        public FollowDeleteDomainServiceTest()
        {
            _followAggregateServiceMock = new Mock<IFollowAggregateService<DummyFollowSession>>();
            _followDeleteDomainService = new FollowDeleteDomainService<DummyFollowSession>(
                _followAggregateServiceMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_正常()
        {
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

            var followId = FollowIdentifier.Reconstruct(userAId, userBId);

            var req = new IFollowDeleteDomainService<DummyFollowSession>.Req()
            {
                FollowId = followId,
                FollowSession = new DummyFollowSession(),
                OperateInfo =
                    new OperateInfo()
                    {
                        OperatedDateTime = new DateTime(2025, 9, 1),
                        Operator = userAId,
                    }
            };

            _followAggregateServiceMock.Setup(
                x => x.GetEntityByIdentifierAsync(
                    It.IsAny<DummyFollowSession>(),
                    followId,
                    It.IsAny<CancellationToken>()));

            await _followDeleteDomainService.ExecuteAsync(req);

            _followAggregateServiceMock.Verify(
                x => x.DeleteAsync(
                    It.Is<IFollowAggregateService<DummyFollowSession>.DeleteReq>(r => r.FollowId == followId),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        public class DummyFollowSession : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
