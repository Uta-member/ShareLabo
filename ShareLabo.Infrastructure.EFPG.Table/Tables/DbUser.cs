using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public class DbUser : VersionedTableBase
    {
        [Comment("アカウントID")]
        [Required]
        [MinLength(1)]
        public required string AccountId { get; set; }

        [Comment("ユーザID")]
        [Required]
        [MinLength(1)]
        public required string UserId { get; set; }

        [Comment("ユーザ名")]
        [Required]
        [MinLength(1)]
        public required string UserName { get; set; }
    }
}
