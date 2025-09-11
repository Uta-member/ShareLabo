using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareLabo.Infrastructure.EFPG.Table.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeLineOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "owner_id",
                table: "time_lines",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "所有ユーザID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "time_lines");
        }
    }
}
