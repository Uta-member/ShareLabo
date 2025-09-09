using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShareLabo.Infrastructure.EFPG.Table.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "group_members");

            migrationBuilder.DropTable(
                name: "groups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "group_members",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<string>(type: "text", nullable: false, comment: "グループID"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID"),
                    user_id = table.Column<string>(type: "text", nullable: false, comment: "メンバーID")
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
        }
    }
}
