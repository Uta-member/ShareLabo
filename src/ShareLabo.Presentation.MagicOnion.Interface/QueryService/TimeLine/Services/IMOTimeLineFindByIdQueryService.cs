using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOTimeLineFindByIdQueryService
        : IMOQueryService<IMOTimeLineFindByIdQueryService, IMOTimeLineFindByIdQueryService.Req, ITimeLineFindByIdQueryService.Req, IMOTimeLineFindByIdQueryService.Res, ITimeLineFindByIdQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<ITimeLineFindByIdQueryService.Req, Req>
        {
            public static Req FromDTO(ITimeLineFindByIdQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public ITimeLineFindByIdQueryService.Req ToDTO()
            {
                return this.Adapt<ITimeLineFindByIdQueryService.Req>();
            }

            [Key(0)]
            public required string TimeLineId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<ITimeLineFindByIdQueryService.Res, Res>
        {
            public static Res FromDTO(ITimeLineFindByIdQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public ITimeLineFindByIdQueryService.Res ToDTO()
            {
                return this.Adapt<ITimeLineFindByIdQueryService.Res>();
            }

            [Key(0)]
            public required MPOptional<MPTimeLineDetailReadModel> TimeLineOptional
            {
                get;
                init;
            }
        }
    }
}
