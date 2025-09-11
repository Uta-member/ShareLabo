using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Post
{
    public sealed class PostUpdateDomainService<TPostSession>
        : IDomainService<PostUpdateDomainService<TPostSession>.Req>
        where TPostSession : IDisposable
    {
        private readonly PostAggregateService<TPostSession> _postAggregateService;

        public PostUpdateDomainService(
            PostAggregateService<TPostSession> postAggregateService)
        {
            _postAggregateService = postAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _postAggregateService.UpdateAsync(
                new PostAggregateService<TPostSession>.UpdateReq()
                {
                    Command =
                        new PostEntity.UpdateCommand()
                            {
                                ContentOptional = req.PostContentOptional,
                                TitleOptional = req.PostTitleOptional,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.PostSession,
                    TargetId = req.TargetPostId,
                },
                cancellationToken);
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required Optional<PostContent> PostContentOptional { get; init; }

            public required TPostSession PostSession { get; init; }

            public required Optional<PostTitle> PostTitleOptional { get; init; }

            public required PostId TargetPostId { get; init; }
        }
    }
}
