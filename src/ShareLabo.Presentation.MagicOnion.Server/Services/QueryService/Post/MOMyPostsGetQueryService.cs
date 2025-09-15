using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.Post
{
    [ExceptionFilter]
    public sealed class MOMyPostsGetQueryService
        : MOQueryServiceBase<IMOMyPostsGetQueryService,
        IMyPostsGetQueryService,
        IMOMyPostsGetQueryService.Req,
        IMyPostsGetQueryService.Req,
        IMOMyPostsGetQueryService.Res,
        IMyPostsGetQueryService.Res>
        ,
        IMOMyPostsGetQueryService
    {
        public MOMyPostsGetQueryService(IMyPostsGetQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOMyPostsGetQueryService.Res> Execute(IMOMyPostsGetQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
