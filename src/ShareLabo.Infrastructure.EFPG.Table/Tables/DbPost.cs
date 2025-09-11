using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public class DbPost : TableBase
    {
        [Comment("投稿内容")]
        [Required]
        [MinLength(1)]
        public required string PostContent { get; set; }

        [Comment("投稿日時")]
        [Required]
        public required DateTime PostDateTime { get; set; }

        [Comment("投稿ID")]
        [Required]
        [MinLength(1)]
        public required string PostId { get; set; }

        [Comment("投稿タイトル")]
        [Required]
        [MinLength(1)]
        public required string PostTitle { get; set; }

        [Comment("投稿者ID")]
        [Required]
        [MinLength(1)]
        public required string PostUserId { get; set; }

        [Comment("連番ID")]
        [Required]
        public required long SequenceId { get; set; }
    }
}
