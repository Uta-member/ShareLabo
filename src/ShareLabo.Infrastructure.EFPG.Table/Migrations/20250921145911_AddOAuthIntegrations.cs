using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShareLabo.Infrastructure.EFPG.Table.Migrations
{
    /// <inheritdoc />
    public partial class AddOAuthIntegrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "o_auth_integrations",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    o_auth_identifier = table.Column<string>(type: "text", nullable: false, comment: "認証ID"),
                    o_auth_type = table.Column<int>(type: "integer", nullable: false, comment: "認証タイプ"),
                    user_id = table.Column<string>(type: "text", nullable: false, comment: "ユーザID"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_o_auth_integrations", x => x.pointer_no);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "o_auth_integrations");
        }
    }
}
