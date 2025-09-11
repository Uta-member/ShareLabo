using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public abstract class VersionedTableBase : TableBase
    {
        [Comment("状態フラグ")]
        public required ConditionFlgEnum ConditionFlg { get; set; }

        [NotMapped]
        public static int InitialSeq => 1;

        [Comment("最新フラグ")]
        public required bool IsLast { get; set; }

        [Comment("バージョン番号")]
        public required int Seq { get; set; }
    }
}
