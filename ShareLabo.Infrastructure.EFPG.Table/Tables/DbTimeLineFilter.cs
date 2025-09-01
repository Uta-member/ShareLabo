using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public sealed class DbTimeLineFilter : TableBase
    {
        [Comment("タイムラインID")]
        [Required]
        [MinLength(1)]
        public required string TimeLineId { get; set; }

        [Comment("ユーザID")]
        [Required]
        [MinLength(1)]
        public required string UserId { get; set; }
    }
}
