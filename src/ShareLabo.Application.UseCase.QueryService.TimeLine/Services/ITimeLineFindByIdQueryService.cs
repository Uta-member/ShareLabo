using CSStack.TADA;

namespace ShareLabo.Application.UseCase.QueryService.TimeLine
{
    public interface ITimeLineFindByIdQueryService
        : IQueryService<ITimeLineFindByIdQueryService.Req, ITimeLineFindByIdQueryService.Res>
    {
        sealed record Req : IQueryServiceDTO
        {
            public required string TimeLineId { get; init; }
        }

        sealed record Res : IQueryServiceDTO
        {
            public Optional<TimeLineDetailReadModel> TimeLineOptional
            {
                get;
                init;
            } = Optional<TimeLineDetailReadModel>.Empty;
        }
    }
}
