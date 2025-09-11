using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public abstract class TableBase
    {
        [Comment("作成日時")]
        public required DateTime InsertTimeStamp { get; set; }

        [Comment("作成者ID")]
        [MinLength(1)]
        public required string InsertUserId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PointerNo { get; set; }

        [Comment("更新日時")]
        public DateTime? UpdateTimeStamp { get; set; }

        [Comment("更新者ID")]
        public string? UpdateUserId { get; set; }
    }
}
