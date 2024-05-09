using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LotteryNumber",
                table: "SearchHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LotteryNumber",
                table: "PurchaseTickets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LotteryNumber",
                table: "Lotteries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0cd291c-bc67-4a23-a15a-36a25b6ea69c", "AQAAAAIAAYagAAAAEJOd2Zf+BZMZrUivP8cjmOlkc1FVhrQ7s1Er97Bo5DTnBHc4G9aVqKtdKmS9WZxIuw==", "2ce3adfd-9005-4bfe-93dc-841d3a2be324" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d03f743-e76b-400b-bfcc-71177296eb1a", "AQAAAAIAAYagAAAAEDAQSs7+EzXTaFBQ7rkdlxizA2NvA5yQWmeudozViD0QUNCWwRrdV4xb2hhecw8s3A==", "0725ada1-99c1-421b-9f64-3f11d630cd90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "228f62aa-f6ff-4be3-828b-1021a4c8d361", "AQAAAAIAAYagAAAAEIuGPp1rJPPEyl7NrZ1uL5cuYz0JL/9OrYGq3QwssJFM+eCunOJPb9AOLtloulKSkg==", "40f45e9c-62eb-4b8c-9c35-f82ab38c26fa" });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 1,
                column: "LotteryNumber",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 2,
                column: "LotteryNumber",
                value: "234567");

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 3,
                column: "LotteryNumber",
                value: "345678");

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 4,
                column: "LotteryNumber",
                value: "456789");

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 5,
                column: "LotteryNumber",
                value: "567890");

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 6,
                column: "LotteryNumber",
                value: "678901");

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 1,
                column: "LotteryNumber",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 2,
                column: "LotteryNumber",
                value: "234567");

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 3,
                column: "LotteryNumber",
                value: "345678");

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 4,
                column: "LotteryNumber",
                value: "456789");

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 1,
                column: "LotteryNumber",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 2,
                column: "LotteryNumber",
                value: "234567");

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 3,
                column: "LotteryNumber",
                value: "345678");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LotteryNumber",
                table: "SearchHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LotteryNumber",
                table: "PurchaseTickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LotteryNumber",
                table: "Lotteries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                column: "LotteryNumber",
                value: 123456);

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 2,
                column: "LotteryNumber",
                value: 234567);

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 3,
                column: "LotteryNumber",
                value: 345678);

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 4,
                column: "LotteryNumber",
                value: 456789);

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 5,
                column: "LotteryNumber",
                value: 567890);

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 6,
                column: "LotteryNumber",
                value: 678901);

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 1,
                column: "LotteryNumber",
                value: 123456);

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 2,
                column: "LotteryNumber",
                value: 234567);

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 3,
                column: "LotteryNumber",
                value: 345678);

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 4,
                column: "LotteryNumber",
                value: 456789);

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 1,
                column: "LotteryNumber",
                value: 123456);

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 2,
                column: "LotteryNumber",
                value: 234567);

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 3,
                column: "LotteryNumber",
                value: 345678);
        }
    }
}
