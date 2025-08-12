using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

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
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    PostContent = dto.PostContent.Value,
                    PostDateTime = dto.PostDateTime,
                    PostId = dto.PostId.Value,
                    PostTitle = dto.PostTitle.Value,
                    PostUserId = dto.PostUserId.Value,
                    PublicationGroups = dto.PublicationGroups.Select(g => g.Value).ToList()
                };
            }

            public IPostCreateCommandService.Req ToDTO()
            {
                return new IPostCreateCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    PostContent = Domain.ValueObject.PostContent.Reconstruct(PostContent),
                    PostDateTime = PostDateTime,
                    PostId = Domain.ValueObject.PostId.Reconstruct(PostId),
                    PostTitle = Domain.ValueObject.PostTitle.Reconstruct(PostTitle),
                    PostUserId = UserId.Reconstruct(PostUserId),
                    PublicationGroups = PublicationGroups.Select(g => GroupId.Reconstruct(g)).ToImmutableList()
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

            [Key(6)]
            public required List<string> PublicationGroups { get; init; }
        }
    }
}
