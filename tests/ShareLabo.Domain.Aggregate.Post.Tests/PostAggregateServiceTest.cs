using CSStack.TADA;
using Moq;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Post.Tests
{
    public sealed class PostAggregateServiceTest
    {
        private readonly IPostAggregateService<DummyPostSession> _postAggregateService;
        private readonly Mock<IPostRepository<DummyPostSession>> _postRepositoryMock;
        private readonly DummyPostSession _postSession;

        public PostAggregateServiceTest()
        {
            _postRepositoryMock = new Mock<IPostRepository<DummyPostSession>>();
            _postAggregateService = new PostAggregateService<DummyPostSession>(_postRepositoryMock.Object);
            _postSession = new DummyPostSession();
        }

        [Fact]
        public async Task CreateAsync_正常()
        {
            var postId = PostId.Create(new string('a', 8));
            var postTitle = PostTitle.Create(new string('a', 8));
            var postContent = PostContent.Create(new string('a', 8));
            var postDateTime = new DateTime(2025, 9, 1);
            var postUserId = UserId.Reconstruct(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = postUserId,
            };

            _postRepositoryMock.Setup(
                x => x.FindLatestPostAsync(It.IsAny<DummyPostSession>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<PostEntity>.Empty);
            _postRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(It.IsAny<DummyPostSession>(), postId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<PostEntity>.Empty);

            await _postAggregateService.CreateAsync(
                new IPostAggregateService<DummyPostSession>.CreateReq()
                {
                    Command =
                        new PostEntity.CreateCommand()
                            {
                                Content = postContent,
                                Id = postId,
                                PostDateTime = postDateTime,
                                PostUser = postUserId,
                                Title = postTitle,
                            },
                    OperateInfo = operateInfo,
                    Session = _postSession,
                });

            _postRepositoryMock.Verify(
                r => r.SaveAsync(
                    _postSession,
                    It.IsAny<PostEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_同一オブジェクト存在()
        {
            var postId = PostId.Create(new string('a', 8));
            var postTitle = PostTitle.Create(new string('a', 8));
            var postContent = PostContent.Create(new string('a', 8));
            var postDateTime = new DateTime(2025, 9, 1);
            var postUserId = UserId.Reconstruct(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = postUserId,
            };

            _postRepositoryMock.Setup(
                x => x.FindLatestPostAsync(It.IsAny<DummyPostSession>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<PostEntity>.Empty);
            _postRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(It.IsAny<DummyPostSession>(), postId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<PostEntity>.Some(It.IsAny<PostEntity>()));

            await Assert.ThrowsAsync<ObjectAlreadyExistException>(
                async () => await _postAggregateService.CreateAsync(
                    new IPostAggregateService<DummyPostSession>.CreateReq()
                {
                    Command =
                        new PostEntity.CreateCommand()
                                {
                                    Content = postContent,
                                    Id = postId,
                                    PostDateTime = postDateTime,
                                    PostUser = postUserId,
                                    Title = postTitle,
                                },
                    OperateInfo = operateInfo,
                    Session = _postSession,
                }));

            _postRepositoryMock.Verify(
                r => r.SaveAsync(
                    _postSession,
                    It.IsAny<PostEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Delete_正常()
        {
            var postId = PostId.Reconstruct(new string('a', 8));
            var postUserId = UserId.Reconstruct(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = postUserId,
            };

            _postRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(It.IsAny<DummyPostSession>(), postId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<PostEntity>.Some(It.IsAny<PostEntity>()));

            await _postAggregateService.DeleteAsync(
                new IPostAggregateService<DummyPostSession>.DeleteReq()
                {
                    OperateInfo = operateInfo,
                    Session = _postSession,
                    TargetId = postId,
                });

            _postRepositoryMock.Verify(
                r => r.DeleteAsync(
                    _postSession,
                    It.IsAny<PostEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Delete_存在しないオブジェクト()
        {
            var postId = PostId.Reconstruct(new string('a', 8));
            var postUserId = UserId.Reconstruct(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = postUserId,
            };

            _postRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(It.IsAny<DummyPostSession>(), postId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<PostEntity>.Empty);

            await Assert.ThrowsAsync<ObjectNotFoundException>(
                async () => await _postAggregateService.DeleteAsync(
                    new IPostAggregateService<DummyPostSession>.DeleteReq()
                {
                    OperateInfo = operateInfo,
                    Session = _postSession,
                    TargetId = postId,
                }));

            _postRepositoryMock.Verify(
                r => r.DeleteAsync(
                    _postSession,
                    It.IsAny<PostEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Update_存在しないオブジェクト()
        {
            var postId = PostId.Create(new string('a', 8));
            var postTitle = PostTitle.Create(new string('a', 8));
            var postContent = PostContent.Create(new string('a', 8));
            var postDateTime = new DateTime(2025, 9, 1);
            var postUserId = UserId.Reconstruct(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = postUserId,
            };

            _postRepositoryMock.Setup(
                x => x.FindLatestPostAsync(It.IsAny<DummyPostSession>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<PostEntity>.Empty);
            _postRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(It.IsAny<DummyPostSession>(), postId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Optional<PostEntity>.Empty);

            await Assert.ThrowsAsync<ObjectNotFoundException>(
                async () => await _postAggregateService.UpdateAsync(
                    new IPostAggregateService<DummyPostSession>.UpdateReq()
                {
                    Command =
                        new PostEntity.UpdateCommand()
                                {
                                    ContentOptional = postContent,
                                    PostDateTimeOptional = postDateTime,
                                    TitleOptional = postTitle,
                                },
                    OperateInfo = operateInfo,
                    Session = _postSession,
                    TargetId = postId,
                }));

            _postRepositoryMock.Verify(
                r => r.SaveAsync(
                    _postSession,
                    It.IsAny<PostEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_正常()
        {
            var postId = PostId.Create(new string('a', 8));
            var postTitle = PostTitle.Create(new string('a', 8));
            var postContent = PostContent.Create(new string('a', 8));
            var postDateTime = new DateTime(2025, 9, 1);
            var postUserId = UserId.Reconstruct(new string('a', 8));

            var operateInfo = new OperateInfo()
            {
                OperatedDateTime = new DateTime(2025, 9, 1),
                Operator = postUserId,
            };

            var postEntity = PostEntity.Reconstruct(
                new PostEntity.ReconstructCommand()
                {
                    Content = postContent,
                    Id = postId,
                    PostDateTime = postDateTime,
                    PostUser = postUserId,
                    SequenceId = 1L,
                    Title = postTitle,
                });

            postTitle = PostTitle.Create(new string('b', 8));
            postContent = PostContent.Create(new string('b', 8));
            postDateTime = new DateTime(2025, 9, 2);

            _postRepositoryMock.Setup(
                x => x.FindLatestPostAsync(It.IsAny<DummyPostSession>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(postEntity);
            _postRepositoryMock.Setup(
                x => x.FindByIdentifierAsync(It.IsAny<DummyPostSession>(), postId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(postEntity);

            await _postAggregateService.UpdateAsync(
                new IPostAggregateService<DummyPostSession>.UpdateReq()
                {
                    Command =
                        new PostEntity.UpdateCommand()
                            {
                                ContentOptional = postContent,
                                PostDateTimeOptional = postDateTime,
                                TitleOptional = postTitle,
                            },
                    OperateInfo = operateInfo,
                    Session = _postSession,
                    TargetId = postId,
                });

            _postRepositoryMock.Verify(
                r => r.SaveAsync(
                    _postSession,
                    It.IsAny<PostEntity>(),
                    operateInfo,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        public sealed record DummyPostSession : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
