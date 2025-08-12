using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public sealed class DbGroupMember : TableBase
    {
        [Comment("グループID")]
        [Required]
        [MinLength(1)]
        public required string GroupId { get; set; }

        [Comment("メンバーID")]
        [Required]
        [MinLength(1)]
        public required string UserId { get; set; }
    }
}
