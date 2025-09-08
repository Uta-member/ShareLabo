using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOGroupUpdateCommandService
        : IMOCommandService<IMOGroupUpdateCommandService, IMOGroupUpdateCommandService.Req, IGroupUpdateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IGroupUpdateCommandService.Req, Req>
        {
            public static Req FromDTO(IGroupUpdateCommandService.Req dto)
            {
                return new Req()
                {
                    GroupNameOptional = MPOptional<string>.FromOptional(dto.GroupNameOptional, x => x.Value),
                    MembersOptional =
                        MPOptional<List<string>>.FromOptional(dto.MembersOptional, x => x.Select(y => y.Value).ToList()),
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    TargetId = dto.TargetId.Value,
                };
            }

            public IGroupUpdateCommandService.Req ToDTO()
            {
                return new IGroupUpdateCommandService.Req()
                {
                    GroupNameOptional = GroupNameOptional.ToOptional(x => GroupName.Reconstruct(x)),
                    MembersOptional =
                        MembersOptional.ToOptional(x => x.Select(y => UserId.Reconstruct(y)).ToImmutableList()),
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetId = GroupId.Reconstruct(TargetId),
                };
            }

            [Key(0)]
            public required MPOptional<string> GroupNameOptional { get; init; }

            [Key(1)]
            public required MPOptional<List<string>> MembersOptional { get; init; }

            [Key(2)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(3)]
            public required string TargetId { get; init; }
        }
    }
}
