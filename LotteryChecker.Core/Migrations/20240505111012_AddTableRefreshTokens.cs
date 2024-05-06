using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddTableRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jwtld = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0184be25-a85f-4a95-8e9e-6c33550e484c", "AQAAAAIAAYagAAAAEI8swDHEalccy0wVWmFDisOQITjOGFCCZx8OdHrVf8PkPdhjP2xoQijegGk2KjxEbA==", "e09e5824-e457-478b-b47e-eced699ea7fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a1686a6-da75-4f13-8a60-3f3c6541353e", "AQAAAAIAAYagAAAAEGwGxk4aWypzUQLzw9dnkibzQXJOZBK0o3tmbqiHJdVTx0SS4ImCefXJGusoU7NUsA==", "6baa4c85-62f3-4ab1-b369-e71832e30d49" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50355b03-3415-4bda-a163-8b13cef9f653", "AQAAAAIAAYagAAAAEJgxHP5TefDu7CFNoKmXS6Vjiy+VAFHM+n2SUw05NrfYtFaiChS3tcAmGpIhmIk92w==", "838bac91-0495-4228-83a7-8a911fd50cb6" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1bbf1d87-8df5-4cd3-9f80-819c0bc56ecb", "AQAAAAIAAYagAAAAEH8MZ1uBFMOYeDfw8I1KJEoUBARzveFKntUP/E5fnLsRBB7406yNd6/tLuShPkLCRA==", "5a03649c-a394-4e04-a0b3-ce4e10a6fda7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5aeab1e7-ad8e-42a3-9845-2a83d689d2eb", "AQAAAAIAAYagAAAAEBfUVENWKnp/dY/gsWg9sCM1FE1OHhoxentFRd13AeXzGRleALVXXlsE1U2TPhriag==", "2c85dd44-81f9-480b-8ae1-2880feae15c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1c601fa-e65e-49a5-9270-68bf42b76b9d", "AQAAAAIAAYagAAAAEAB3JJFF9C5+2QWoom/4wGUF9m/MYxvGCx5gOYXd+eAgdPurVwI16TXFDrXESc1+mg==", "673541d2-fd19-4c65-86c5-ac325bd806fa" });
        }
    }
}
