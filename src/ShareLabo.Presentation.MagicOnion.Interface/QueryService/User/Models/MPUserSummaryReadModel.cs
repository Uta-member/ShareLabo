using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPUserSummaryReadModel : IMPDTO<UserSummaryReadModel, MPUserSummaryReadModel>
    {
        public static MPUserSummaryReadModel FromDTO(UserSummaryReadModel dto)
        {
            return new MPUserSummaryReadModel()
            {
                Status = dto.Status,
                UserAccountId = dto.UserAccountId,
                UserId = dto.UserId,
                UserName = dto.UserName,
            };
        }

        public UserSummaryReadModel ToDTO()
        {
            return new UserSummaryReadModel()
            {
                Status = Status,
                UserAccountId = UserAccountId,
                UserId = UserId,
                UserName = UserName,
            };
        }

        [Key(0)]
        public required UserEntity.StatusEnum Status { get; init; }

        [Key(1)]
        public required string UserAccountId { get; init; }

        [Key(2)]
        public required string UserId { get; init; }

        [Key(3)]
        public required string UserName { get; init; }
    }
}
