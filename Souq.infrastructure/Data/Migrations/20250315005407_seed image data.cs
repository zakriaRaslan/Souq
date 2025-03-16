using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Souq.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedimagedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "ImageName", "ProductId" },
                values: new object[] { 1, "test", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
