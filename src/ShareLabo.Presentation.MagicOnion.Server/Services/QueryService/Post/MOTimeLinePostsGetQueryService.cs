using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.Post
{
    public sealed class MOTimeLinePostsGetQueryService
        : MOQueryServiceBase<IMOTimeLinePostsGetQueryService, ITimeLinePostsGetQueryService, IMOTimeLinePostsGetQueryService.Req, ITimeLinePostsGetQueryService.Req, IMOTimeLinePostsGetQueryService.Res, ITimeLinePostsGetQueryService.Res>
        , IMOTimeLinePostsGetQueryService
    {
        public MOTimeLinePostsGetQueryService(ITimeLinePostsGetQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOTimeLinePostsGetQueryService.Res> Execute(
            IMOTimeLinePostsGetQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
