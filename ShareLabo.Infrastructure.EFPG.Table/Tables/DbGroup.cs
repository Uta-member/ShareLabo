using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public sealed class DbGroup : TableBase
    {
        [Comment("グループID")]
        [Required]
        [MinLength(1)]
        public required string GroupId { get; set; }

        [Comment("グループ名")]
        [Required]
        [MinLength(1)]
        public required string GroupName { get; set; }
    }
}
