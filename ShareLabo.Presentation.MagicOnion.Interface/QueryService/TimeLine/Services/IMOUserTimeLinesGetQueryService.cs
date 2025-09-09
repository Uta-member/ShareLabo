using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;
using System.Collections.Immutable;

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
                return new Req()
                {
                    UserId = dto.UserId,
                };
            }

            public IUserTimeLinesGetQueryService.Req ToDTO()
            {
                return new IUserTimeLinesGetQueryService.Req()
                {
                    UserId = UserId,
                };
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserTimeLinesGetQueryService.Res, Res>
        {
            public static Res FromDTO(IUserTimeLinesGetQueryService.Res dto)
            {
                return new Res()
                {
                    TimeLineSummaries =
                        dto.TimeLineSummaries.Select(x => MPTimeLineSummaryReadModel.FromDTO(x)).ToList(),
                };
            }

            public IUserTimeLinesGetQueryService.Res ToDTO()
            {
                return new IUserTimeLinesGetQueryService.Res()
                {
                    TimeLineSummaries = TimeLineSummaries.Select(x => x.ToDTO()).ToImmutableList(),
                };
            }

            [Key(0)]
            public required List<MPTimeLineSummaryReadModel> TimeLineSummaries { get; init; }
        }
    }
}
