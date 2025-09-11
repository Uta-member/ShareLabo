using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public class DbTimeLine : TableBase
    {
        [Comment("所有ユーザID")]
        [Required]
        [MinLength(1)]
        public required string OwnerId { get; set; }

        [Comment("タイムラインID")]
        [Required]
        [MinLength(1)]
        public required string TimeLineId { get; set; }

        [Comment("タイムライン名")]
        [Required]
        [MinLength(1)]
        public required string TimeLineName { get; set; }
    }
}
