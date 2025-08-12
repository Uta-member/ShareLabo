using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPOperateInfo : IMPDTO<OperateInfo, MPOperateInfo>
    {
        public static MPOperateInfo FromDTO(OperateInfo dto)
        {
            return new MPOperateInfo()
            {
                OperatedDateTime = dto.OperatedDateTime,
                Operator = dto.Operator.Value,
            };
        }

        public OperateInfo ToDTO()
        {
            return new OperateInfo()
            {
                OperatedDateTime = OperatedDateTime,
                Operator = UserId.Reconstruct(Operator),
            };
        }

        [Key(0)]
        public required DateTime OperatedDateTime { get; init; }

        [Key(1)]
        public required string Operator { get; init; }
    }
}
