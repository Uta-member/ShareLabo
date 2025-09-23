using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOPostCreateCommandService
        : IMOCommandService<IMOPostCreateCommandService, IMOPostCreateCommandService.Req, IPostCreateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IPostCreateCommandService.Req, Req>
        {
            public static Req FromDTO(IPostCreateCommandService.Req dto)
            {
                return new Req()
                {
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                    PostContent = dto.PostContent,
                    PostDateTime = dto.PostDateTime,
                    PostId = dto.PostId,
                    PostTitle = dto.PostTitle,
                    PostUserId = dto.PostUserId,
                };
            }

            public IPostCreateCommandService.Req ToDTO()
            {
                return new IPostCreateCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    PostContent = PostContent,
                    PostDateTime = PostDateTime,
                    PostId = PostId,
                    PostTitle = PostTitle,
                    PostUserId = PostUserId,
                };
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string PostContent { get; init; }

            [Key(2)]
            public required DateTime PostDateTime { get; init; }

            [Key(3)]
            public required string PostId { get; init; }

            [Key(4)]
            public required string PostTitle { get; init; }

            [Key(5)]
            public required string PostUserId { get; init; }
        }
    }
}
