using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Domain.DomainService.Post;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.Post
{
    public sealed class PostDeleteCommandService<TPostSession> : IPostDeleteCommandService
        where TPostSession : IDisposable
    {
        private readonly IPostDeleteDomainService<TPostSession> _postDeleteDomainService;
        private readonly ITransactionManager _transactionManager;

        public PostDeleteCommandService(
            ITransactionManager transactionManager,
            IPostDeleteDomainService<TPostSession> postDeleteDomainService)
        {
            _transactionManager = transactionManager;
            _postDeleteDomainService = postDeleteDomainService;
        }

        public async ValueTask ExecuteAsync(
            IPostDeleteCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TPostSession) ],
                async sessions => await _postDeleteDomainService.ExecuteAsync(
                    new IPostDeleteDomainService<TPostSession>.Req()
                    {
                        OperateInfo = req.OperateInfo.ToOperateInfo(),
                        PostSession = sessions.GetSession<TPostSession>(),
                        TargetPostId = PostId.Reconstruct(req.TargetPostId),
                    }));
        }
    }
}
