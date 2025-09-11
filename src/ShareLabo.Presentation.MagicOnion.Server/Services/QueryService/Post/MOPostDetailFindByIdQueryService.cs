using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.Post
{
    public sealed class MOPostDetailFindByIdQueryService
        : MOQueryServiceBase<IMOPostDetailFindByIdQueryService,
        IPostDetailFindByIdQueryService,
        IMOPostDetailFindByIdQueryService.Req,
        IPostDetailFindByIdQueryService.Req,
        IMOPostDetailFindByIdQueryService.Res,
        IPostDetailFindByIdQueryService.Res>
        , IMOPostDetailFindByIdQueryService
    {
        public MOPostDetailFindByIdQueryService(IPostDetailFindByIdQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOPostDetailFindByIdQueryService.Res> Execute(
            IMOPostDetailFindByIdQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
