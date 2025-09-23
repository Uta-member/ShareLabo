using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.Toolkit
{
    public sealed record OperateInfoDTO
    {
        public OperateInfo ToOperateInfo()
        {
            return new OperateInfo()
            {
                OperatedDateTime = OperatedDateTime,
                Operator = UserId.Reconstruct(Operator),
            };
        }

        public required DateTime OperatedDateTime { get; init; }

        public required string Operator { get; init; }
    }
}
