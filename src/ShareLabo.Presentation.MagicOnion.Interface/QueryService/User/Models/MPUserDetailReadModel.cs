using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPUserDetailReadModel : IMPDTO<UserDetailReadModel, MPUserDetailReadModel>
    {
        public static MPUserDetailReadModel FromDTO(UserDetailReadModel dto)
        {
            return dto.Adapt<MPUserDetailReadModel>();
        }

        public UserDetailReadModel ToDTO()
        {
            return this.Adapt<UserDetailReadModel>();
        }

        [Key(0)]
        public required DateTime InsertTimeStamp { get; init; }

        [Key(1)]
        public required string InsertUserId { get; init; }

        [Key(2)]
        public required string InsertUserName { get; init; }

        [Key(3)]
        public required UserEntity.StatusEnum Status { get; init; }

        [Key(4)]
        public DateTime? UpdateTimeStamp { get; init; }

        [Key(5)]
        public string? UpdateUserId { get; init; }

        [Key(6)]
        public string? UpdateUserName { get; init; }

        [Key(7)]
        public required string UserAccountId { get; init; }

        [Key(8)]
        public required string UserId { get; init; }

        [Key(9)]
        public required string UserName { get; init; }
    }
}
