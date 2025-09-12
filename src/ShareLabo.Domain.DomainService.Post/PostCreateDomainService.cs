using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Domain.DomainService.Post
{
    public sealed class PostCreateDomainService<TPostSession, TUserSession>
        : IPostCreateDomainService<TPostSession, TUserSession>
        where TPostSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly IPostAggregateService<TPostSession> _postAggregateService;
        private readonly IUserAggregateService<TUserSession> _userAggregateService;

        public PostCreateDomainService(
            IPostAggregateService<TPostSession> postAggregateService,
            IUserAggregateService<TUserSession> userAggregateService)
        {
            _postAggregateService = postAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IPostCreateDomainService<TPostSession, TUserSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            var postUserOptional = await _userAggregateService.GetEntityByIdentifierAsync(
                req.UserSession,
                req.PostUser,
                cancellationToken);
            if(!postUserOptional.HasValue)
            {
                throw new ObjectNotFoundException("投稿者のユーザが見つかりません");
            }

            await _postAggregateService.CreateAsync(
                new IPostAggregateService<TPostSession>.CreateReq()
                {
                    Command =
                        new PostEntity.CreateCommand()
                            {
                                Content = req.PostContent,
                                Id = req.PostId,
                                PostDateTime = req.PostDateTime,
                                PostUser = req.PostUser,
                                Title = req.PostTitle,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.PostSession,
                },
                cancellationToken);
        }
    }
}
