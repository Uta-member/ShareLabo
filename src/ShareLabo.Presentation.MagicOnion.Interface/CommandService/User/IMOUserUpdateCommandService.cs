using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.User;

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
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                    TargetUserId = dto.TargetUserId,
                    UserAccountIdOptional = dto.UserAccountIdOptional.ToMPOptional(),
                    UserNameOptional = dto.UserNameOptional.ToMPOptional(),
                };
            }

            public IUserUpdateCommandService.Req ToDTO()
            {
                return new IUserUpdateCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetUserId = TargetUserId,
                    UserAccountIdOptional = UserAccountIdOptional.ToOptional(),
                    UserNameOptional = UserNameOptional.ToOptional(),
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
