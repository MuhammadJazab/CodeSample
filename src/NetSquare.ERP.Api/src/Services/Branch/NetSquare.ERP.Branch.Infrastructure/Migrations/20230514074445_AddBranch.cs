using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetSquare.ERP.Branch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "erp",
                columns: table => new
                {
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BranchCity = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchManager = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branch",
                schema: "erp");
        }
    }
}
