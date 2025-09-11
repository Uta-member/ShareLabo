using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Domain.DomainService.Post;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.Post
{
    public sealed class PostCreateCommandService<TPostSession, TUserSession> : IPostCreateCommandService
        where TPostSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly PostCreateDomainService<TPostSession, TUserSession> _postCreateDomainService;
        private readonly ITransactionManager _transactionManager;

        public PostCreateCommandService(
            ITransactionManager transactionManager,
            PostCreateDomainService<TPostSession, TUserSession> postCreateDomainService)
        {
            _transactionManager = transactionManager;
            _postCreateDomainService = postCreateDomainService;
        }

        public async ValueTask ExecuteAsync(
            IPostCreateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TPostSession), typeof(TUserSession)],
                async sessions => await _postCreateDomainService.ExecuteAsync(
                    new PostCreateDomainService<TPostSession, TUserSession>.Req()
                {
                    OperateInfo = req.OperateInfo,
                    PostContent = req.PostContent,
                    PostDateTime = req.PostDateTime,
                    PostId = req.PostId,
                    PostSession = sessions.GetSession<TPostSession>(),
                    PostTitle = req.PostTitle,
                    PostUser = req.PostUserId,
                    UserSession = sessions.GetSession<TUserSession>()
                },
                    cancellationToken));
        }
    }
}
