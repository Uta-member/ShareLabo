using CSStack.TADA.MagicOnionHelper.Abstractions;
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
                return new Req()
                {
                    TimeLineId = dto.TimeLineId,
                };
            }

            public ITimeLineFindByIdQueryService.Req ToDTO()
            {
                return new ITimeLineFindByIdQueryService.Req()
                {
                    TimeLineId = TimeLineId,
                };
            }

            [Key(0)]
            public required string TimeLineId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<ITimeLineFindByIdQueryService.Res, Res>
        {
            public static Res FromDTO(ITimeLineFindByIdQueryService.Res dto)
            {
                return new Res()
                {
                    TimeLineOptional = dto.TimeLineOptional.ToMPOptional(x => MPTimeLineDetailReadModel.FromDTO(x)),
                };
            }

            public ITimeLineFindByIdQueryService.Res ToDTO()
            {
                return new ITimeLineFindByIdQueryService.Res()
                {
                    TimeLineOptional = TimeLineOptional.ToOptional(x => x.ToDTO()),
                };
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
