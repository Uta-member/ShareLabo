using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOGroupCreateCommandService
        : IMOCommandService<IMOGroupCreateCommandService, IMOGroupCreateCommandService.Req, IGroupCreateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IGroupCreateCommandService.Req, Req>
        {
            public static Req FromDTO(IGroupCreateCommandService.Req dto)
            {
                return new Req()
                {
                    GroupId = dto.GroupId.Value,
                    GroupName = dto.GroupName.Value,
                    Members = dto.Members.Select(x => x.Value).ToList(),
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                };
            }

            public IGroupCreateCommandService.Req ToDTO()
            {
                return new IGroupCreateCommandService.Req()
                {
                    GroupId = Domain.ValueObject.GroupId.Reconstruct(GroupId),
                    GroupName = Domain.ValueObject.GroupName.Reconstruct(GroupName),
                    Members = Members.Select(x => UserId.Reconstruct(x)).ToImmutableList(),
                    OperateInfo = OperateInfo.ToDTO(),
                };
            }

            [Key(0)]
            public required string GroupId { get; init; }

            [Key(1)]
            public required string GroupName { get; init; }

            [Key(2)]
            public required List<string> Members { get; init; }

            [Key(3)]
            public required MPOperateInfo OperateInfo { get; init; }
        }
    }
}
