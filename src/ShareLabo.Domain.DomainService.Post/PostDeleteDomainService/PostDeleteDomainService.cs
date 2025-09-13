using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Post;

namespace ShareLabo.Domain.DomainService.Post
{
    public sealed class PostDeleteDomainService<TPostSession>
        : IDomainService<IPostDeleteDomainService<TPostSession>.Req>
        where TPostSession : IDisposable
    {
        private readonly IPostAggregateService<TPostSession> _postAggregateService;

        public PostDeleteDomainService(IPostAggregateService<TPostSession> postAggregateService)
        {
            _postAggregateService = postAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IPostDeleteDomainService<TPostSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            await _postAggregateService.DeleteAsync(
                new IPostAggregateService<TPostSession>.DeleteReq()
                {
                    OperateInfo = req.OperateInfo,
                    Session = req.PostSession,
                    TargetId = req.TargetPostId
                });
        }
    }
}
