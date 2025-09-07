using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public sealed class DbFollow : TableBase
    {
        [Comment("フォロー開始日時")]
        [Required]
        public required DateTime FollowStartDateTime { get; set; }

        [Comment("フォロー元ユーザID")]
        [Required]
        [MinLength(1)]
        public required string FromUserId { get; set; }

        [Comment("フォロー先ユーザID")]
        [Required]
        [MinLength(1)]
        public required string ToUserId { get; set; }
    }
}
