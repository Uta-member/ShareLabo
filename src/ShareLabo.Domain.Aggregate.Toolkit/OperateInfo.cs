using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Toolkit
{
    public sealed record OperateInfo
    {
        public required DateTime OperatedDateTime { get; init; }

        public required UserId Operator { get; init; }
    }
}
