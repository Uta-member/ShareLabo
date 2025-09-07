using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.Post
{
    public sealed class MOFollowedPostsGetQueryService
        : MOQueryServiceBase<IMOFollowedPostsGetQueryService,
        IFollowedPostsGetQueryService,
        IMOFollowedPostsGetQueryService.Req,
        IFollowedPostsGetQueryService.Req,
        IMOFollowedPostsGetQueryService.Res,
        IFollowedPostsGetQueryService.Res>
        ,
        IMOFollowedPostsGetQueryService
    {
        public MOFollowedPostsGetQueryService(IFollowedPostsGetQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOFollowedPostsGetQueryService.Res> Execute(
            IMOFollowedPostsGetQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
