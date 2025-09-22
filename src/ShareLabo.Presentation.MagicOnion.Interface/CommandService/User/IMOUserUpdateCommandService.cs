using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
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
                return dto.Adapt<Req>();
            }

            public IUserUpdateCommandService.Req ToDTO()
            {
                return this.Adapt<IUserUpdateCommandService.Req>();
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
