using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Follow;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserFollowersGetQueryService
        : IMOQueryService<IMOUserFollowersGetQueryService,
        IMOUserFollowersGetQueryService.Req, IUserFollowersGetQueryService.Req, IMOUserFollowersGetQueryService.Res, IUserFollowersGetQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserFollowersGetQueryService.Req, Req>
        {
            public static Req FromDTO(IUserFollowersGetQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IUserFollowersGetQueryService.Req ToDTO()
            {
                return this.Adapt<IUserFollowersGetQueryService.Req>();
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserFollowersGetQueryService.Res, Res>
        {
            public static Res FromDTO(IUserFollowersGetQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public IUserFollowersGetQueryService.Res ToDTO()
            {
                return this.Adapt<IUserFollowersGetQueryService.Res>();
            }

            [Key(0)]
            public required List<MPFollowReadModel> Followers { get; init; }
        }
    }
}
