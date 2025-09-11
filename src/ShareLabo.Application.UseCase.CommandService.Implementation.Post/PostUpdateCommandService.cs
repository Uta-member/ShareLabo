using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Domain.DomainService.Post;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.Post
{
    public sealed class PostUpdateCommandService<TPostSession> : IPostUpdateCommandService
        where TPostSession : IDisposable
    {
        private readonly PostUpdateDomainService<TPostSession> _postUpdateDomainService;
        private readonly ITransactionManager _transactionManager;

        public PostUpdateCommandService(
            ITransactionManager transactionManager,
            PostUpdateDomainService<TPostSession> postUpdateDomainService)
        {
            _transactionManager = transactionManager;
            _postUpdateDomainService = postUpdateDomainService;
        }

        public async ValueTask ExecuteAsync(
            IPostUpdateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TPostSession)],
                async sessions => await _postUpdateDomainService.ExecuteAsync(
                    new PostUpdateDomainService<TPostSession>.Req()
                {
                    OperateInfo = req.OperateInfo,
                    PostContentOptional = req.PostContentOptional,
                    PostSession = sessions.GetSession<TPostSession>(),
                    PostTitleOptional = req.PostTitleOptional,
                    TargetPostId = req.TargetPostId,
                }));
        }
    }
}
