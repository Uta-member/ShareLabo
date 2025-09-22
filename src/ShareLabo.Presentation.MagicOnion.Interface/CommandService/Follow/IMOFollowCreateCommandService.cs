using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
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
                return dto.Adapt<Req>();
            }

            public IFollowCreateCommandService.Req ToDTO()
            {
                return this.Adapt<IFollowCreateCommandService.Req>();
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
