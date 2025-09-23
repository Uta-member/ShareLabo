using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
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
                        dto.FilterMembersOptional.ToMPOptional(x => x.ToList()),
                    NameOptional = dto.NameOptional.ToMPOptional(),
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                    TargetId = dto.TargetId,
                };
            }

            public ITimeLineUpdateCommandService.Req ToDTO()
            {
                return new ITimeLineUpdateCommandService.Req()
                {
                    FilterMembersOptional =
                        FilterMembersOptional.ToOptional(x => x.ToImmutableList()),
                    NameOptional = NameOptional.ToOptional(),
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetId = TargetId,
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
