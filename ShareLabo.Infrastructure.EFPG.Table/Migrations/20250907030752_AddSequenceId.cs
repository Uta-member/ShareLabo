using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareLabo.Infrastructure.EFPG.Table.Migrations
{
    /// <inheritdoc />
    public partial class AddSequenceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "sequence_id",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "連番ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sequence_id",
                table: "posts");
        }
    }
}
