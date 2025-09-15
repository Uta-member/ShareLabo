using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.Post
{
    [ExceptionFilter]
    public sealed class MOGeneralPostsGetQueryService
        : MOQueryServiceBase<IMOGeneralPostsGetQueryService, IGeneralPostsGetQueryService, IMOGeneralPostsGetQueryService.Req, IGeneralPostsGetQueryService.Req, IMOGeneralPostsGetQueryService.Res, IGeneralPostsGetQueryService.Res>
        , IMOGeneralPostsGetQueryService
    {
        public MOGeneralPostsGetQueryService(IGeneralPostsGetQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOGeneralPostsGetQueryService.Res> Execute(IMOGeneralPostsGetQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
