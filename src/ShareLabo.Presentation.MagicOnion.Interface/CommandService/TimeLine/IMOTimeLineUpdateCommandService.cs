using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.TimeLine;

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
                return dto.Adapt<Req>();
            }

            public ITimeLineUpdateCommandService.Req ToDTO()
            {
                return this.Adapt<ITimeLineUpdateCommandService.Req>();
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
