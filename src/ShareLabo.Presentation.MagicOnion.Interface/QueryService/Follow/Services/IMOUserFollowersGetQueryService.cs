using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Follow;
using System.Collections.Immutable;

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
                return new Req()
                {
                    UserId = dto.UserId,
                };
            }

            public IUserFollowersGetQueryService.Req ToDTO()
            {
                return new IUserFollowersGetQueryService.Req()
                {
                    UserId = UserId,
                };
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserFollowersGetQueryService.Res, Res>
        {
            public static Res FromDTO(IUserFollowersGetQueryService.Res dto)
            {
                return new Res()
                {
                    Followers = dto.Followers.Select(x => MPFollowReadModel.FromDTO(x)).ToList(),
                };
            }

            public IUserFollowersGetQueryService.Res ToDTO()
            {
                return new IUserFollowersGetQueryService.Res()
                {
                    Followers = Followers.Select(x => x.ToDTO()).ToImmutableList(),
                };
            }

            [Key(0)]
            public required List<MPFollowReadModel> Followers { get; init; }
        }
    }
}
