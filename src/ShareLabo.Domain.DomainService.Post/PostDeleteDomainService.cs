using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Post
{
    public sealed class PostDeleteDomainService<TPostSession>
        : IDomainService<PostDeleteDomainService<TPostSession>.Req>
        where TPostSession : IDisposable
    {
        private readonly PostAggregateService<TPostSession> _postAggregateService;

        public PostDeleteDomainService(PostAggregateService<TPostSession> postAggregateService)
        {
            _postAggregateService = postAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _postAggregateService.DeleteAsync(
                new PostAggregateService<TPostSession>.DeleteReq()
                {
                    OperateInfo = req.OperateInfo,
                    Session = req.PostSession,
                    TargetId = req.TargetPostId
                });
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TPostSession PostSession { get; init; }

            public required PostId TargetPostId { get; init; }
        }
    }
}
