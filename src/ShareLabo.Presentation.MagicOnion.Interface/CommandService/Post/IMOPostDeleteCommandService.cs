using CSStack.TADA.MagicOnionHelper.Abstractions;
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
                return new Req()
                {
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                    TargetPostId = dto.TargetPostId,
                };
            }

            public IPostDeleteCommandService.Req ToDTO()
            {
                return new IPostDeleteCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetPostId = TargetPostId,
                };
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string TargetPostId { get; init; }
        }
    }
}
