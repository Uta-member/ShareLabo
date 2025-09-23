using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPOperateInfo : IMPDTO<OperateInfoDTO, MPOperateInfo>
    {
        public static MPOperateInfo FromDTO(OperateInfoDTO dto)
        {
            return new MPOperateInfo
            {
                OperatedDateTime = dto.OperatedDateTime,
                Operator = dto.Operator,
            };
        }

        public OperateInfoDTO ToDTO()
        {
            return new OperateInfoDTO
            {
                OperatedDateTime = OperatedDateTime,
                Operator = Operator,
            };
        }

        [Key(0)]
        public required DateTime OperatedDateTime { get; init; }

        [Key(1)]
        public required string Operator { get; init; }
    }
}
