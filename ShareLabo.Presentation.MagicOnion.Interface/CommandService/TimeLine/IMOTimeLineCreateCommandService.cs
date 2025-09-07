using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommanService.TimeLine;
using ShareLabo.Domain.ValueObject;
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
                    FilterMembers = dto.FilterMembers.Select(x => x.Value).ToList(),
                    Id = dto.Id.Value,
                    Name = dto.Name.Value,
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    OwnerId = dto.OwnerId.Value,
                };
            }

            public ITimeLineCreateCommandService.Req ToDTO()
            {
                return new ITimeLineCreateCommandService.Req()
                {
                    FilterMembers = FilterMembers.Select(x => UserId.Reconstruct(x)).ToImmutableList(),
                    Id = TimeLineId.Reconstruct(Id),
                    Name = TimeLineName.Reconstruct(Name),
                    OperateInfo = OperateInfo.ToDTO(),
                    OwnerId = UserId.Reconstruct(OwnerId),
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
