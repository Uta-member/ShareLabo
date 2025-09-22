using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
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
                return dto.Adapt<Req>();
            }

            public IPostUpdateCommandService.Req ToDTO()
            {
                return this.Adapt<IPostUpdateCommandService.Req>();
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
