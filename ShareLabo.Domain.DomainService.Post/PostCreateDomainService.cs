using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.DomainService.Post
{
    public sealed class PostCreateDomainService<TPostSession, TUserSession, TGroupSession>
        : IDomainService<PostCreateDomainService<TPostSession, TUserSession, TGroupSession>.Req>
        where TPostSession : IDisposable
        where TUserSession : IDisposable
        where TGroupSession : IDisposable
    {
        private readonly GroupAggregateService<TGroupSession> _groupAggregateService;
        private readonly PostAggregateService<TPostSession> _postAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public PostCreateDomainService(
            GroupAggregateService<TGroupSession> groupAggregateService,
            PostAggregateService<TPostSession> postAggregateService,
            UserAggregateService<TUserSession> userAggregateService)
        {
            _groupAggregateService = groupAggregateService;
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

            foreach(var group in req.PublicationGroups)
            {
                var groupEntityOptional = await _groupAggregateService.GetEntityByIdentifierAsync(
                    req.GroupSession,
                    group,
                    cancellationToken);
                if(!groupEntityOptional.HasValue)
                {
                    throw new ObjectNotFoundException($"公開先グループ {group} が見つかりません");
                }
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
                                PublicationGroups = req.PublicationGroups,
                                Title = req.PostTitle,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.PostSession,
                },
                cancellationToken);
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required TGroupSession GroupSession { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required PostContent PostContent { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required PostId PostId { get; init; }

            public required TPostSession PostSession { get; init; }

            public required PostTitle PostTitle { get; init; }

            public required UserId PostUser { get; init; }

            public required ImmutableList<GroupId> PublicationGroups { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
