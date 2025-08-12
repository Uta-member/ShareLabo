using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public sealed class DbAccount : VersionedTableBase
    {
        [Comment("アカウントID")]
        [Required]
        [MinLength(1)]
        public required string AccountId { get; set; }

        [Comment("パスワードハッシュ")]
        [Required]
        [MinLength(1)]
        public required string PasswordHash { get; set; }

        [Comment("ユーザID")]
        [Required]
        [MinLength(1)]
        public required string UserId { get; set; }
    }
}
