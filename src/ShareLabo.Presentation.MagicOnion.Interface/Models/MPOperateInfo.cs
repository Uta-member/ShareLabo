using MessagePack;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPOperateInfo
    {
        [Key(0)]
        public required DateTime OperatedDateTime { get; init; }

        [Key(1)]
        public required string Operator { get; init; }
    }
}
