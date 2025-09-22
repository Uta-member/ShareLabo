using Microsoft.EntityFrameworkCore;
using ShareLabo.Application.Authentication.OAuthIntegration;
using System.ComponentModel.DataAnnotations;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public class DbOAuthIntegration : TableBase
    {
        [Comment("認証ID")]
        [Required]
        [MinLength(1)]
        public required string OAuthIdentifier { get; set; }

        [Comment("認証タイプ")]
        [Required]
        public required OAuthType OAuthType { get; set; }

        [Comment("ユーザID")]
        [Required]
        [MinLength(1)]
        public required string UserId { get; set; }
    }
}
