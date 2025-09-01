using CSStack.TADA;
using ShareLabo.Domain.DomainService.Post;

namespace ShareLabo.Application.UseCase.CommandService.Post
{
    public sealed class PostCreateCommandService<TPostSession, TUserSession, TGroupSession> : IPostCreateCommandService
        where TPostSession : IDisposable
        where TUserSession : IDisposable
        where TGroupSession : IDisposable
    {
        private readonly PostCreateDomainService<TPostSession, TUserSession, TGroupSession> _postCreateDomainService;
        private readonly ITransactionManager _transactionManager;

        public PostCreateCommandService(
            ITransactionManager transactionManager,
            PostCreateDomainService<TPostSession, TUserSession, TGroupSession> postCreateDomainService)
        {
            _transactionManager = transactionManager;
            _postCreateDomainService = postCreateDomainService;
        }

        public async ValueTask ExecuteAsync(
            IPostCreateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TPostSession), typeof(TUserSession), typeof(TGroupSession)],
                async sessions => await _postCreateDomainService.ExecuteAsync(
                    new PostCreateDomainService<TPostSession, TUserSession, TGroupSession>.Req()
                {
                    GroupSession = sessions.GetSession<TGroupSession>(),
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
