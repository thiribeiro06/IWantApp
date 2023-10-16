using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWantApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEditedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Products",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Categories",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Categories");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
