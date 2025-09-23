using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOPostUpdateCommandService
        : IMOCommandService<IMOPostUpdateCommandService, IMOPostUpdateCommandService.Req, IPostUpdateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IPostUpdateCommandService.Req, Req>
        {
            public static Req FromDTO(IPostUpdateCommandService.Req dto)
            {
                return new Req()
                {
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                    PostContentOptional = dto.PostContentOptional.ToMPOptional(),
                    PostTitleOptional = dto.PostTitleOptional.ToMPOptional(),
                    TargetPostId = dto.TargetPostId,
                };
            }

            public IPostUpdateCommandService.Req ToDTO()
            {
                return new IPostUpdateCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    PostContentOptional = PostContentOptional.ToOptional(),
                    PostTitleOptional = PostTitleOptional.ToOptional(),
                    TargetPostId = TargetPostId,
                };
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required MPOptional<string> PostContentOptional { get; init; }

            [Key(2)]
            public required MPOptional<string> PostTitleOptional { get; init; }

            [Key(3)]
            public required string TargetPostId { get; init; }
        }
    }
}
