using ShareLabo.Domain.Aggregate.Post;

namespace ShareLabo.Domain.DomainService.Post
{
    public sealed class PostUpdateDomainService<TPostSession>
        : IPostUpdateDomainService<TPostSession>
        where TPostSession : IDisposable
    {
        private readonly IPostAggregateService<TPostSession> _postAggregateService;

        public PostUpdateDomainService(
            IPostAggregateService<TPostSession> postAggregateService)
        {
            _postAggregateService = postAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IPostUpdateDomainService<TPostSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            await _postAggregateService.UpdateAsync(
                new IPostAggregateService<TPostSession>.UpdateReq()
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
    }
}
