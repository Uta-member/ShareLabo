using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.TimeLine;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOTimeLineCreateCommandService
        : IMOCommandService<IMOTimeLineCreateCommandService, IMOTimeLineCreateCommandService.Req, ITimeLineCreateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<ITimeLineCreateCommandService.Req, Req>
        {
            public static Req FromDTO(ITimeLineCreateCommandService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public ITimeLineCreateCommandService.Req ToDTO()
            {
                return this.Adapt<ITimeLineCreateCommandService.Req>();
            }

            [Key(0)]
            public required List<string> FilterMembers { get; init; }

            [Key(1)]
            public required string Id { get; init; }

            [Key(2)]
            public required string Name { get; init; }

            [Key(3)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(4)]
            public required string OwnerId { get; init; }
        }
    }
}
