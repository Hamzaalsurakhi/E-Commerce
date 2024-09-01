using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulky.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class AddFKCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[] { 2, 2, "SciFi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[] { 22, 2, "SciFi" });
        }
    }
}
