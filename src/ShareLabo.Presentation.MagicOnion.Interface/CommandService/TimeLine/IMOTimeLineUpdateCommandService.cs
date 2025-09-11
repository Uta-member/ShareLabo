using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOTimeLineUpdateCommandService
        : IMOCommandService<IMOTimeLineUpdateCommandService, IMOTimeLineUpdateCommandService.Req, ITimeLineUpdateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<ITimeLineUpdateCommandService.Req, Req>
        {
            public static Req FromDTO(ITimeLineUpdateCommandService.Req dto)
            {
                return new Req()
                {
                    FilterMembersOptional =
                        MPOptional<List<string>>.FromOptional(
                            dto.FilterMembersOptional,
                            x => x.Select(y => y.Value).ToList()),
                    NameOptional = MPOptional<string>.FromOptional(dto.NameOptional, x => x.Value),
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    TargetId = dto.TargetId.Value,
                };
            }

            public ITimeLineUpdateCommandService.Req ToDTO()
            {
                return new ITimeLineUpdateCommandService.Req()
                {
                    FilterMembersOptional =
                        FilterMembersOptional.ToOptional(x => x.Select(y => UserId.Reconstruct(y)).ToImmutableList()),
                    NameOptional = NameOptional.ToOptional(x => TimeLineName.Reconstruct(x)),
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetId = TimeLineId.Reconstruct(TargetId),
                };
            }

            [Key(0)]
            public required MPOptional<List<string>> FilterMembersOptional { get; init; }

            [Key(1)]
            public required MPOptional<string> NameOptional { get; init; }

            [Key(2)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(3)]
            public required string TargetId { get; init; }
        }
    }
}
