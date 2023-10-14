using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BSApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60917bb8-65f8-490d-a79e-d55003192df5", "29a36dd9-e817-4041-ab0e-ef70d9c48f9c", "Admin", "ADMIN" },
                    { "b69c5e7d-6c74-4397-a49e-e6635a49de02", "22617b07-840a-479d-9528-bebfc6985ef4", "Editor", "EDITOR" },
                    { "fc2d04b2-1fd2-4a4f-bce6-c470205f8b90", "ec1d6a1e-7c28-4dea-8103-b3c8ab36e35b", "DefaultAppUser", "DEFAULTAPPUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60917bb8-65f8-490d-a79e-d55003192df5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b69c5e7d-6c74-4397-a49e-e6635a49de02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc2d04b2-1fd2-4a4f-bce6-c470205f8b90");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers");

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
    }
}
