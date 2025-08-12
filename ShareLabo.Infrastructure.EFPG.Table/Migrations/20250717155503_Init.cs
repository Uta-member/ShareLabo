using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShareLabo.Infrastructure.EFPG.Table.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_id = table.Column<string>(type: "text", nullable: false, comment: "アカウントID"),
                    password_hash = table.Column<string>(type: "text", nullable: false, comment: "パスワードハッシュ"),
                    user_id = table.Column<string>(type: "text", nullable: false, comment: "ユーザID"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID"),
                    condition_flg = table.Column<int>(type: "integer", nullable: false, comment: "状態フラグ"),
                    is_last = table.Column<bool>(type: "boolean", nullable: false, comment: "最新フラグ"),
                    seq = table.Column<int>(type: "integer", nullable: false, comment: "バージョン番号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.pointer_no);
                });

            migrationBuilder.CreateTable(
                name: "group_members",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<string>(type: "text", nullable: false, comment: "グループID"),
                    user_id = table.Column<string>(type: "text", nullable: false, comment: "メンバーID"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_group_members", x => x.pointer_no);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<string>(type: "text", nullable: false, comment: "グループID"),
                    group_name = table.Column<string>(type: "text", nullable: false, comment: "グループ名"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_groups", x => x.pointer_no);
                });

            migrationBuilder.CreateTable(
                name: "post_publications",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<string>(type: "text", nullable: false, comment: "グループID"),
                    post_id = table.Column<string>(type: "text", nullable: false, comment: "投稿ID"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_publications", x => x.pointer_no);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    post_content = table.Column<string>(type: "text", nullable: false, comment: "投稿内容"),
                    post_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "投稿日時"),
                    post_id = table.Column<string>(type: "text", nullable: false, comment: "投稿ID"),
                    post_title = table.Column<string>(type: "text", nullable: false, comment: "投稿タイトル"),
                    post_user_id = table.Column<string>(type: "text", nullable: false, comment: "投稿者ID"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.pointer_no);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_id = table.Column<string>(type: "text", nullable: false, comment: "アカウントID"),
                    user_id = table.Column<string>(type: "text", nullable: false, comment: "ユーザID"),
                    user_name = table.Column<string>(type: "text", nullable: false, comment: "ユーザ名"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID"),
                    condition_flg = table.Column<int>(type: "integer", nullable: false, comment: "状態フラグ"),
                    is_last = table.Column<bool>(type: "boolean", nullable: false, comment: "最新フラグ"),
                    seq = table.Column<int>(type: "integer", nullable: false, comment: "バージョン番号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.pointer_no);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "group_members");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "post_publications");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
