using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Culturize.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CompanyDeactivatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivatedAt",
                table: "Companies",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeactivatedAt",
                table: "Companies");
        }
    }
}
