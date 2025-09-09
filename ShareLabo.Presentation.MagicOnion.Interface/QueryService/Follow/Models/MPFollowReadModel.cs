using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Follow;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPFollowReadModel : IMPDTO<FollowReadModel, MPFollowReadModel>
    {
        public static MPFollowReadModel FromDTO(FollowReadModel dto)
        {
            return new MPFollowReadModel()
            {
                AccountId = dto.AccountId,
                FollowStartDateTime = dto.FollowStartDateTime,
                UserId = dto.UserId,
                UserName = dto.UserName,
            };
        }

        public FollowReadModel ToDTO()
        {
            return new FollowReadModel()
            {
                AccountId = AccountId,
                FollowStartDateTime = FollowStartDateTime,
                UserId = UserId,
                UserName = UserName,
            };
        }

        [Key(0)]
        public required string AccountId { get; init; }

        [Key(1)]
        public required DateTime FollowStartDateTime { get; init; }

        [Key(2)]
        public required string UserId { get; init; }

        [Key(3)]
        public required string UserName { get; init; }
    }
}
