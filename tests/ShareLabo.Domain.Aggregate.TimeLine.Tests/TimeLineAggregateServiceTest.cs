using CSStack.TADA;
using Moq;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.TimeLine.Tests
{
    public sealed class TimeLineAggregateServiceTest
    {
        private readonly ITimeLineAggregateService<TimeLineDummySession> _timeLineAggregateService;
        private readonly Mock<ITimeLineRepository<TimeLineDummySession>> _timeLineRepositoryMock;
        private readonly TimeLineDummySession _timeLineSession;

        public TimeLineAggregateServiceTest()
        {
            _timeLineRepositoryMock = new Mock<ITimeLineRepository<TimeLineDummySession>>();
            _timeLineAggregateService = new TimeLineAggregateService<TimeLineDummySession>(
                _timeLineRepositoryMock.Object);
            _timeLineSession = new TimeLineDummySession();
        }

        [Fact]
        public async Task CreateAsync_正常()
        {
            var timeLineId = TimeLineId.Create(new string('a', 8));
            var timeLineName = TimeLineName.Create(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            ImmutableList<UserId> timeLineFilters = [];
            for(var i = 0; i < 50; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = ownerId,
            };

            _timeLineRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(
                    _timeLineSession,
                    timeLineId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    Optional<TimeLineEntity>.Empty);

            await _timeLineAggregateService.CreateAsync(
                new ITimeLineAggregateService<TimeLineDummySession>.CreateReq()
                {
                    Command =
                        new TimeLineEntity.CreateCommand()
                            {
                                FilterMembers = timeLineFilters,
                                Id = timeLineId,
                                Name = timeLineName,
                                OwnerId = ownerId,
                            },
                    OperateInfo = operateInfo,
                    Session = _timeLineSession,
                });

            _timeLineRepositoryMock.Verify(
                x => x.SaveAsync(
                    _timeLineSession,
                    It.IsAny<TimeLineEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_同一オブジェクト存在()
        {
            var timeLineId = TimeLineId.Create(new string('a', 8));
            var timeLineName = TimeLineName.Create(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            ImmutableList<UserId> timeLineFilters = [];
            for(var i = 0; i < 50; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = ownerId,
            };

            _timeLineRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(
                    _timeLineSession,
                    timeLineId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    Optional<TimeLineEntity>.Some(It.IsAny<TimeLineEntity>()));

            await Assert.ThrowsAsync<ObjectAlreadyExistException>(
                async () => await _timeLineAggregateService.CreateAsync(
                    new ITimeLineAggregateService<TimeLineDummySession>.CreateReq()
                {
                    Command =
                        new TimeLineEntity.CreateCommand()
                                {
                                    FilterMembers = timeLineFilters,
                                    Id = timeLineId,
                                    Name = timeLineName,
                                    OwnerId = ownerId,
                                },
                    OperateInfo = operateInfo,
                    Session = _timeLineSession,
                }));

            _timeLineRepositoryMock.Verify(
                x => x.SaveAsync(
                    _timeLineSession,
                    It.IsAny<TimeLineEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_正常()
        {
            var timeLineId = TimeLineId.Reconstruct(new string('a', 8));
            var timeLineName = TimeLineName.Reconstruct(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            ImmutableList<UserId> timeLineFilters = [];
            for(var i = 0; i < 50; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = ownerId,
            };

            var timeLineEntity = TimeLineEntity.Reconstruct(
                new TimeLineEntity.ReconstructCommand()
                {
                    FilterMembers = timeLineFilters,
                    Id = timeLineId,
                    Name = timeLineName,
                    OwnerId = ownerId,
                });

            _timeLineRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(_timeLineSession, timeLineId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(timeLineEntity);

            await _timeLineAggregateService.DeleteAsync(
                new ITimeLineAggregateService<TimeLineDummySession>.DeleteReq()
                {
                    OperateInfo = operateInfo,
                    Session = _timeLineSession,
                    TargetId = timeLineId,
                });

            _timeLineRepositoryMock.Verify(
                x => x.DeleteAsync(_timeLineSession, timeLineEntity, operateInfo, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_存在しないオブジェクト()
        {
            var timeLineId = TimeLineId.Reconstruct(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = ownerId,
            };

            _timeLineRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(_timeLineSession, timeLineId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<TimeLineEntity>.Empty);

            await Assert.ThrowsAsync<ObjectNotFoundException>(
                async () => await _timeLineAggregateService.DeleteAsync(
                new ITimeLineAggregateService<TimeLineDummySession>.DeleteReq()
                {
                    OperateInfo = operateInfo,
                    Session = _timeLineSession,
                    TargetId = timeLineId,
                }));

            _timeLineRepositoryMock.Verify(
                x => x.DeleteAsync(
                    _timeLineSession,
                    It.IsAny<TimeLineEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_正常()
        {
            var timeLineId = TimeLineId.Create(new string('a', 8));
            var timeLineName = TimeLineName.Create(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            ImmutableList<UserId> timeLineFilters = [];
            for(var i = 0; i < 50; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = ownerId,
            };

            var timeLineEntity = TimeLineEntity.Reconstruct(
                new TimeLineEntity.ReconstructCommand()
                {
                    FilterMembers = timeLineFilters,
                    Id = timeLineId,
                    Name = timeLineName,
                    OwnerId = ownerId,
                });

            _timeLineRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(
                    _timeLineSession,
                    timeLineId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    Optional<TimeLineEntity>.Some(timeLineEntity));

            timeLineName = TimeLineName.Create(new string('b', 8));

            for(var i = 50; i < 51; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            await _timeLineAggregateService.UpdateAsync(
                new ITimeLineAggregateService<TimeLineDummySession>.UpdateReq()
                {
                    Command =
                        new TimeLineEntity.UpdateCommand()
                            {
                                FilterMembersOptional = timeLineFilters,
                                NameOptional = timeLineName,
                            },
                    OperateInfo = operateInfo,
                    Session = _timeLineSession,
                    TargetId = timeLineId,
                });

            _timeLineRepositoryMock.Verify(
                x => x.SaveAsync(
                    _timeLineSession,
                    It.IsAny<TimeLineEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_存在しないオブジェクト()
        {
            var timeLineId = TimeLineId.Create(new string('a', 8));
            var timeLineName = TimeLineName.Create(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            ImmutableList<UserId> timeLineFilters = [];
            for(var i = 0; i < 50; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = ownerId,
            };

            _timeLineRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(
                    _timeLineSession,
                    timeLineId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    Optional<TimeLineEntity>.Empty);

            await Assert.ThrowsAsync<ObjectNotFoundException>(
                async () => await _timeLineAggregateService.UpdateAsync(
                    new ITimeLineAggregateService<TimeLineDummySession>.UpdateReq()
                {
                    Command =
                        new TimeLineEntity.UpdateCommand()
                                {
                                    FilterMembersOptional = timeLineFilters,
                                    NameOptional = timeLineName,
                                },
                    OperateInfo = operateInfo,
                    Session = _timeLineSession,
                    TargetId = timeLineId,
                }));

            _timeLineRepositoryMock.Verify(
                x => x.SaveAsync(
                    _timeLineSession,
                    It.IsAny<TimeLineEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        public sealed record TimeLineDummySession : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
