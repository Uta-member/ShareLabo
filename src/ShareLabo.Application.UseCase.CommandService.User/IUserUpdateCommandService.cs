using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface IUserUpdateCommandService : ICommandService<IUserUpdateCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required OperateInfoWriteModel OperateInfo { get; init; }

            public required string TargetUserId { get; init; }

            public required Optional<string> UserAccountIdOptional
            {
                get;
                init;
            } = Optional<string>.Empty;

            public Optional<string> UserNameOptional { get; init; } = Optional<string>.Empty;
        }
    }
}
