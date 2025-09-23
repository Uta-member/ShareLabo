using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Follow;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOFollowCreateCommandService
        : IMOCommandService<IMOFollowCreateCommandService, IMOFollowCreateCommandService.Req, IFollowCreateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IFollowCreateCommandService.Req, Req>
        {
            public static Req FromDTO(IFollowCreateCommandService.Req dto)
            {
                return new Req()
                {
                    FollowId = dto.FollowId.ToMPDTO(),
                    FollowStartDateTime = dto.FollowStartDateTime,
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                };
            }

            public IFollowCreateCommandService.Req ToDTO()
            {
                return new IFollowCreateCommandService.Req()
                {
                    FollowId = FollowId.ToDTO(),
                    FollowStartDateTime = FollowStartDateTime,
                    OperateInfo = OperateInfo.ToDTO(),
                };
            }

            [Key(0)]
            public required MPFollowIdentifier FollowId { get; init; }

            [Key(1)]
            public required DateTime FollowStartDateTime { get; init; }

            [Key(3)]
            public required MPOperateInfo OperateInfo { get; init; }
        }
    }
}
