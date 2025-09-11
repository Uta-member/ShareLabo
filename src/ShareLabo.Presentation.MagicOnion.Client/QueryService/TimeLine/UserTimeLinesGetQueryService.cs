using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class UserTimeLinesGetQueryService
        : MOSHQueryServiceClientBase<IMOUserTimeLinesGetQueryService, IMOUserTimeLinesGetQueryService.Req, IUserTimeLinesGetQueryService.Req, IMOUserTimeLinesGetQueryService.Res, IUserTimeLinesGetQueryService.Res>
        , IUserTimeLinesGetQueryService
    {
        public UserTimeLinesGetQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
