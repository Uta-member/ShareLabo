using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOPostDeleteCommandService
        : IMOCommandService<IMOPostDeleteCommandService, IMOPostDeleteCommandService.Req, IPostDeleteCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IPostDeleteCommandService.Req, Req>
        {
            public static Req FromDTO(IPostDeleteCommandService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IPostDeleteCommandService.Req ToDTO()
            {
                return ToDTO().Adapt<IPostDeleteCommandService.Req>();
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string TargetPostId { get; init; }
        }
    }
}
