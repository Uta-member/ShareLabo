using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using System.Collections.Immutable;

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
                return new Req()
                {
                    FilterMembers = dto.FilterMembers.ToList(),
                    Id = dto.Id,
                    Name = dto.Name,
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                    OwnerId = dto.OwnerId,
                };
            }

            public ITimeLineCreateCommandService.Req ToDTO()
            {
                return new ITimeLineCreateCommandService.Req()
                {
                    FilterMembers = FilterMembers.ToImmutableList(),
                    Id = Id,
                    Name = Name,
                    OperateInfo = OperateInfo.ToDTO(),
                    OwnerId = OwnerId,
                };
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
