using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Domain.DomainService.Post;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.Post
{
    public sealed class PostUpdateCommandService<TPostSession> : IPostUpdateCommandService
        where TPostSession : IDisposable
    {
        private readonly IPostUpdateDomainService<TPostSession> _postUpdateDomainService;
        private readonly ITransactionManager _transactionManager;

        public PostUpdateCommandService(
            ITransactionManager transactionManager,
            IPostUpdateDomainService<TPostSession> postUpdateDomainService)
        {
            _transactionManager = transactionManager;
            _postUpdateDomainService = postUpdateDomainService;
        }

        public async ValueTask ExecuteAsync(
            IPostUpdateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TPostSession) ],
                async sessions => await _postUpdateDomainService.ExecuteAsync(
                    new IPostUpdateDomainService<TPostSession>.Req()
                    {
                        OperateInfo = req.OperateInfo.ToOperateInfo(),
                        PostContentOptional =
                            req.PostContentOptional.TryGetValue(out var postContent)
                                        ? PostContent.Create(postContent)
                                        : Optional<PostContent>.Empty,
                        PostSession = sessions.GetSession<TPostSession>(),
                        PostTitleOptional =
                            req.PostTitleOptional.TryGetValue(out var postTitle)
                                        ? PostTitle.Create(postTitle)
                                        : Optional<PostTitle>.Empty,
                        TargetPostId = PostId.Reconstruct(req.TargetPostId),
                    }));
        }
    }
}
