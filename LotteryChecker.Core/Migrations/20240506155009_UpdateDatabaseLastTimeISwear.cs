using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseLastTimeISwear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Lotteries",
                newName: "DrawDate");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7718a05c-1b2f-496a-ab17-256405156985", "AQAAAAIAAYagAAAAEOrmRTbDn0g7tEILlP9QIQdzXgcNkNarHqhsokymRgUJtEx9Lj25347FzTNcjWyEgQ==", "eeeccaa0-17f0-4835-86ec-bc73d173b03b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1333f54-abfb-4197-9208-87cb58787b69", "AQAAAAIAAYagAAAAEBBnxu1iLYFwC+mKfbfQ/vgdedRusTe/Yg6CxZFpg4XCbR78t6ayV0lAzrWBhIQJiw==", "79f66796-69f4-4cd1-bbb5-af3b241342f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc4e6463-378d-4074-b5b1-c4f23fa84933", "AQAAAAIAAYagAAAAECZxMh2Zzrww+cJmnGbUVgubNgsJmIIgIUVSvw3eWQ0ozmLZoxSHsyiHTub2XDq3pg==", "d2173649-751f-49d5-a65a-a907f10de43d" });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 1,
                columns: new[] { "DrawDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 2,
                columns: new[] { "DrawDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 3,
                columns: new[] { "DrawDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 4,
                columns: new[] { "DrawDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 5,
                columns: new[] { "DrawDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 6,
                column: "PublishDate",
                value: new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrawDate",
                table: "Lotteries",
                newName: "DueDate");

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

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 1,
                columns: new[] { "DueDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 2,
                columns: new[] { "DueDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 3,
                columns: new[] { "DueDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 4,
                columns: new[] { "DueDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 5,
                columns: new[] { "DueDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 6,
                column: "PublishDate",
                value: null);
        }
    }
}
