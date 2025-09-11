using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserUpdateCommandService
        : IMOCommandService<IMOUserUpdateCommandService, IMOUserUpdateCommandService.Req, IUserUpdateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserUpdateCommandService.Req, Req>
        {
            public static Req FromDTO(IUserUpdateCommandService.Req dto)
            {
                return new Req()
                {
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    UserAccountIdOptional = MPOptional<string>.FromOptional(dto.UserAccountIdOptional, x => x.Value),
                    TargetUserId = dto.TargetUserId.Value,
                    UserNameOptional = MPOptional<string>.FromOptional(dto.UserNameOptional, x => x.Value),
                };
            }

            public IUserUpdateCommandService.Req ToDTO()
            {
                return new IUserUpdateCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    UserAccountIdOptional = UserAccountIdOptional.ToOptional(x => UserAccountId.Reconstruct(x)),
                    UserNameOptional = UserNameOptional.ToOptional(x => UserName.Reconstruct(x)),
                    TargetUserId = UserId.Reconstruct(TargetUserId),
                };
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string TargetUserId { get; init; }

            [Key(2)]
            public required MPOptional<string> UserAccountIdOptional
            {
                get;
                init;
            }

            [Key(3)]
            public required MPOptional<string> UserNameOptional { get; init; }
        }
    }
}
