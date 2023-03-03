using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarListApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersAndroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "138a30c7-362e-4f90-b240-f7d5bd8ba424", "1a3689a2-e991-4078-95b1-b772112af71b", "User", "USER" },
                    { "3c34c713-d0d7-484d-9ec7-ed1471536072", "e7cef26d-33b4-461e-a0b7-61335d414623", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "36721706-1151-48f9-8983-95ab58ccb376", 0, "b0d17445-2d4c-432b-bf6b-dd2a7d022f0f", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAEAACcQAAAAEPL8G3SmvhXJkpSjzwA6WG2uS7neMz3odQowLRqltnNSJtreshJ3btSRUWb43Xbcbw==", null, false, "02b29152-4076-4806-b523-9a857d7f9518", false, "admin@localhost.com" },
                    { "78f7e47e-e6e4-48bf-adba-a60efc5fdb29", 0, "d4ec9dec-c952-46ec-8f30-6c1fcc2c2dc3", "user@localhost.com", true, false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAELooZcNIxSC+Dmnv48zl77I2tfGwMeFUZ5fvtVPuZXjkNtU6saxqliV53BNuVIkKiA==", null, false, "a51738d3-5f6f-461d-b95a-f2b5c3832ddf", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3c34c713-d0d7-484d-9ec7-ed1471536072", "36721706-1151-48f9-8983-95ab58ccb376" },
                    { "138a30c7-362e-4f90-b240-f7d5bd8ba424", "78f7e47e-e6e4-48bf-adba-a60efc5fdb29" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3c34c713-d0d7-484d-9ec7-ed1471536072", "36721706-1151-48f9-8983-95ab58ccb376" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "138a30c7-362e-4f90-b240-f7d5bd8ba424", "78f7e47e-e6e4-48bf-adba-a60efc5fdb29" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "138a30c7-362e-4f90-b240-f7d5bd8ba424");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c34c713-d0d7-484d-9ec7-ed1471536072");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36721706-1151-48f9-8983-95ab58ccb376");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78f7e47e-e6e4-48bf-adba-a60efc5fdb29");
        }
    }
}
