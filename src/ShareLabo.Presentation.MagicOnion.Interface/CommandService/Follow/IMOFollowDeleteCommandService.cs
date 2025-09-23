using CSStack.TADA.MagicOnionHelper.Abstractions;
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
                return new Req()
                {
                    FollowId = dto.FollowId.ToMPDTO(),
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                };
            }

            public IFollowDeleteCommandService.Req ToDTO()
            {
                return new IFollowDeleteCommandService.Req()
                {
                    FollowId = FollowId.ToDTO(),
                    OperateInfo = OperateInfo.ToDTO(),
                };
            }

            [Key(0)]
            public required MPFollowIdentifier FollowId { get; init; }

            [Key(1)]
            public required MPOperateInfo OperateInfo { get; init; }
        }
    }
}
