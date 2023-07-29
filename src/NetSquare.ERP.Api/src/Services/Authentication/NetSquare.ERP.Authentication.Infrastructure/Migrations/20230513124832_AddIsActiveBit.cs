using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetSquare.ERP.Authentication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveBit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "erp",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "erp",
                table: "AspNetUsers");
        }
    }
}
