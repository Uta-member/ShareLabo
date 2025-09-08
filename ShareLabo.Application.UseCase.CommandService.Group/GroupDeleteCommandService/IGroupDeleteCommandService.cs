using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Group
{
    public interface IGroupDeleteCommandService : ICommandService<IGroupDeleteCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required GroupId TargetId { get; init; }
        }
    }
}
