using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
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
            return dto.Adapt<MPUserSummaryReadModel>();
        }

        public UserSummaryReadModel ToDTO()
        {
            return this.Adapt<UserSummaryReadModel>();
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
