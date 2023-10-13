using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BSApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0df6f1eb-1cde-41ac-8a1e-50b303f58f78", "aaaaf4e6-46c3-47cc-a88d-447137f49a2f", "Editor", "EDITOR" },
                    { "4b13b099-e036-46c3-8cc0-3ac954a4e7c8", "e0ab723e-0315-4c8d-bbd9-2fcd9b63ea3b", "DefaultAppUser", "DEFAULTAPPUSER" },
                    { "f9e22ee5-b5b4-4251-9d8f-6aa5fa401b5e", "f6155df6-1320-47ee-9573-a97fe2d4b92e", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0df6f1eb-1cde-41ac-8a1e-50b303f58f78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b13b099-e036-46c3-8cc0-3ac954a4e7c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9e22ee5-b5b4-4251-9d8f-6aa5fa401b5e");
        }
    }
}
