using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Domain.ValueObject;

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
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    PostContentOptional = MPOptional<string>.FromOptional(dto.PostContentOptional, x => x.Value),
                    PostTitleOptional = MPOptional<string>.FromOptional(dto.PostTitleOptional, x => x.Value),
                    TargetPostId = dto.TargetPostId.Value,
                };
            }

            public IPostUpdateCommandService.Req ToDTO()
            {
                return new IPostUpdateCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    PostContentOptional = PostContentOptional.ToOptional(x => PostContent.Reconstruct(x)),
                    PostTitleOptional = PostTitleOptional.ToOptional(x => PostTitle.Reconstruct(x)),
                    TargetPostId = PostId.Reconstruct(TargetPostId),
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
