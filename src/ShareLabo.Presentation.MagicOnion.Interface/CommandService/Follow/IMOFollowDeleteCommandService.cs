using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Follow;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOFollowDeleteCommandService
        : IMOCommandService<IMOFollowDeleteCommandService, IMOFollowDeleteCommandService.Req, IFollowDeleteCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IFollowDeleteCommandService.Req, Req>
        {
            public static Req FromDTO(IFollowDeleteCommandService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IFollowDeleteCommandService.Req ToDTO()
            {
                return this.Adapt<IFollowDeleteCommandService.Req>();
            }

            [Key(0)]
            public required MPFollowIdentifier FollowId { get; init; }

            [Key(1)]
            public required MPOperateInfo OperateInfo { get; init; }
        }
    }
}
