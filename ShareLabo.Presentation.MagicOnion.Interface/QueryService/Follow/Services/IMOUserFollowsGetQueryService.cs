using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Follow;
using System.Collections.Immutable;

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
                return new Req()
                {
                    UserId = dto.UserId,
                };
            }

            public IUserFollowsGetQueryService.Req ToDTO()
            {
                return new IUserFollowsGetQueryService.Req()
                {
                    UserId = UserId,
                };
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserFollowsGetQueryService.Res, Res>
        {
            public static Res FromDTO(IUserFollowsGetQueryService.Res dto)
            {
                return new Res()
                {
                    Follows = dto.Follows.Select(x => MPFollowReadModel.FromDTO(x)).ToList(),
                };
            }

            public IUserFollowsGetQueryService.Res ToDTO()
            {
                return new IUserFollowsGetQueryService.Res()
                {
                    Follows = Follows.Select(x => x.ToDTO()).ToImmutableList(),
                };
            }

            [Key(0)]
            public required List<MPFollowReadModel> Follows { get; init; }
        }
    }
}
