using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Post
{
    public sealed class PostCreateDomainService<TPostSession, TUserSession>
        : IDomainService<PostCreateDomainService<TPostSession, TUserSession>.Req>
        where TPostSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly PostAggregateService<TPostSession> _postAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public PostCreateDomainService(
            PostAggregateService<TPostSession> postAggregateService,
            UserAggregateService<TUserSession> userAggregateService)
        {
            _postAggregateService = postAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
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
                new PostAggregateService<TPostSession>.CreateReq()
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

        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required PostContent PostContent { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required PostId PostId { get; init; }

            public required TPostSession PostSession { get; init; }

            public required PostTitle PostTitle { get; init; }

            public required UserId PostUser { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
