using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Follow;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserFollowsGetQueryService
        : IMOQueryService<IMOUserFollowsGetQueryService, IMOUserFollowsGetQueryService.Req, IUserFollowsGetQueryService.Req, IMOUserFollowsGetQueryService.Res, IUserFollowsGetQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserFollowsGetQueryService.Req, Req>
        {
            public static Req FromDTO(IUserFollowsGetQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IUserFollowsGetQueryService.Req ToDTO()
            {
                return this.Adapt<IUserFollowsGetQueryService.Req>();
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserFollowsGetQueryService.Res, Res>
        {
            public static Res FromDTO(IUserFollowsGetQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public IUserFollowsGetQueryService.Res ToDTO()
            {
                return this.Adapt<IUserFollowsGetQueryService.Res>();
            }

            [Key(0)]
            public required List<MPFollowReadModel> Follows { get; init; }
        }
    }
}
