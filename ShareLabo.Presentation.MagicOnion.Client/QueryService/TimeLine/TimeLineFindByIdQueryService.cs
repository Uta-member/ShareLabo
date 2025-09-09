using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class TimeLineFindByIdQueryService
        : MOSHQueryServiceClientBase<IMOTimeLineFindByIdQueryService, IMOTimeLineFindByIdQueryService.Req, ITimeLineFindByIdQueryService.Req, IMOTimeLineFindByIdQueryService.Res, ITimeLineFindByIdQueryService.Res>
        , ITimeLineFindByIdQueryService
    {
        public TimeLineFindByIdQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
