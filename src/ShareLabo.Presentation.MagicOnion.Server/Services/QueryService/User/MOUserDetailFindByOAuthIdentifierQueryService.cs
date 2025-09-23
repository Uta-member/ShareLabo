using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.User
{
    public sealed class MOUserDetailFindByOAuthIdentifierQueryService
        : MOQueryServiceBase<IMOUserDetailFindByOAuthIdentifierQueryService, IUserDetailFindByOAuthIdentifierQueryService, IMOUserDetailFindByOAuthIdentifierQueryService.Req, IUserDetailFindByOAuthIdentifierQueryService.Req, IMOUserDetailFindByOAuthIdentifierQueryService.Res, IUserDetailFindByOAuthIdentifierQueryService.Res>
        , IMOUserDetailFindByOAuthIdentifierQueryService
    {
        public MOUserDetailFindByOAuthIdentifierQueryService(IUserDetailFindByOAuthIdentifierQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOUserDetailFindByOAuthIdentifierQueryService.Res> Execute(
            IMOUserDetailFindByOAuthIdentifierQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
