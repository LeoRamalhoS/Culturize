using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Culturize.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyBlobNameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "Companies",
                newName: "LogoBlobFile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoBlobFile",
                table: "Companies",
                newName: "LogoUrl");
        }
    }
}
