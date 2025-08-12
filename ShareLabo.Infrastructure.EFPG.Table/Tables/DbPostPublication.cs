using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public sealed class DbPostPublication : TableBase
    {
        [Comment("グループID")]
        [Required]
        [MinLength(1)]
        public required string GroupId { get; set; }

        [Comment("投稿ID")]
        [Required]
        [MinLength(1)]
        public required string PostId { get; set; }
    }
}
