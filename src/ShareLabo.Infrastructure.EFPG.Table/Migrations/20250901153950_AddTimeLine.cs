using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShareLabo.Infrastructure.EFPG.Table.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "time_line_filters",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time_line_id = table.Column<string>(type: "text", nullable: false, comment: "タイムラインID"),
                    user_id = table.Column<string>(type: "text", nullable: false, comment: "ユーザID"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time_line_filters", x => x.pointer_no);
                });

            migrationBuilder.CreateTable(
                name: "time_lines",
                columns: table => new
                {
                    pointer_no = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time_line_id = table.Column<string>(type: "text", nullable: false, comment: "タイムラインID"),
                    time_line_name = table.Column<string>(type: "text", nullable: false, comment: "タイムライン名"),
                    insert_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "作成日時"),
                    insert_user_id = table.Column<string>(type: "text", nullable: false, comment: "作成者ID"),
                    update_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "更新日時"),
                    update_user_id = table.Column<string>(type: "text", nullable: true, comment: "更新者ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time_lines", x => x.pointer_no);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "time_line_filters");

            migrationBuilder.DropTable(
                name: "time_lines");
        }
    }
}
