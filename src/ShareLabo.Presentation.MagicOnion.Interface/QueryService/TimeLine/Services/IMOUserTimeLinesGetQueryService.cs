using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserTimeLinesGetQueryService
        : IMOQueryService<IMOUserTimeLinesGetQueryService, IMOUserTimeLinesGetQueryService.Req, IUserTimeLinesGetQueryService.Req, IMOUserTimeLinesGetQueryService.Res, IUserTimeLinesGetQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserTimeLinesGetQueryService.Req, Req>
        {
            public static Req FromDTO(IUserTimeLinesGetQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IUserTimeLinesGetQueryService.Req ToDTO()
            {
                return this.Adapt<IUserTimeLinesGetQueryService.Req>();
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserTimeLinesGetQueryService.Res, Res>
        {
            public static Res FromDTO(IUserTimeLinesGetQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public IUserTimeLinesGetQueryService.Res ToDTO()
            {
                return this.Adapt<IUserTimeLinesGetQueryService.Res>();
            }

            [Key(0)]
            public required List<MPTimeLineSummaryReadModel> TimeLineSummaries { get; init; }
        }
    }
}
