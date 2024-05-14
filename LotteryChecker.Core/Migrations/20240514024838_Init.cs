using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Jwtld",
                table: "RefreshTokens",
                newName: "JwtId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5f85084-50a7-41e7-a58a-35bed091bf93", "AQAAAAIAAYagAAAAEE37HpqfzDKfjqFRstKRKlbfgU9G6RcOXCwF5j+ULOnGAItp86uwAlLScCo+M8dyQA==", "a34a9a07-d7ee-4c32-b097-1b881756c7e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b531c6c4-d60a-4195-bae3-f247a8489bc1", "AQAAAAIAAYagAAAAEGUFUVbrxRSezmWd2ewTKxgHVbkG262SnwsdEEwpGuKuGgI0QkwZEfGr/qkjtWnRQw==", "406feaeb-7c53-484d-bd83-357f408951a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6ccf5c8-54f0-4406-86c4-d022ee26aba1", "AQAAAAIAAYagAAAAELlZi54+zq1jBcIX5ogfWu7ml9PyLUJO4y41/BaPqmn50xf2vqPamHfLx3Y5l+hH/g==", "d06af497-c3c3-49c5-93b7-16c1fce6b526" });

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 1,
                column: "DrawDate",
                value: new DateTime(2024, 5, 14, 9, 48, 38, 5, DateTimeKind.Local).AddTicks(2797));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 2,
                column: "DrawDate",
                value: new DateTime(2024, 5, 14, 9, 48, 38, 5, DateTimeKind.Local).AddTicks(2807));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 3,
                column: "DrawDate",
                value: new DateTime(2024, 5, 14, 9, 48, 38, 5, DateTimeKind.Local).AddTicks(2809));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JwtId",
                table: "RefreshTokens",
                newName: "Jwtld");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42f0a52c-eed0-4c9d-8f0a-3be84dba74c9", "AQAAAAIAAYagAAAAEEOyR0NM6fPG075LfIyQQDrxCEVAzxc876TdOrxGF+hvAouuMKxpTks/TXOwuTJEKA==", "6245f13c-3985-4318-a387-3977c22db7e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3762301-fb2e-4435-b4ac-e6feb3d35a65", "AQAAAAIAAYagAAAAEJCMrv4O9e3kKRuaJ3kSHdYHgunVLWDOfl0++766MuFVDC7RLTcuMh2m3h5fgalhmQ==", "ccd92ea2-f3c3-479e-8dbb-b6440ec0db98" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8152ca82-d169-42ad-9818-9c8981580398", "AQAAAAIAAYagAAAAEFqhZcnWVLZAqDdagqoXp/VxL6DZIbnIdTRzd4gu85l3nxv1mt4fbsH1vq3dcYGnwg==", "94070b7d-6f40-44f9-a9b4-a0d027afe459" });

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 1,
                column: "DrawDate",
                value: new DateTime(2024, 5, 12, 16, 29, 50, 970, DateTimeKind.Local).AddTicks(8590));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 2,
                column: "DrawDate",
                value: new DateTime(2024, 5, 12, 16, 29, 50, 970, DateTimeKind.Local).AddTicks(8605));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 3,
                column: "DrawDate",
                value: new DateTime(2024, 5, 12, 16, 29, 50, 970, DateTimeKind.Local).AddTicks(8607));
        }
    }
}
