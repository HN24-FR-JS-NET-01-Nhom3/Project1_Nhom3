using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddPrizeForSearchHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prize",
                table: "SearchHistories",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8da3917-0f61-4556-96ca-2847daee1d04", "AQAAAAIAAYagAAAAEKLQpNK1A4uwCmLsV6teOfZCIVkcxec+rwM+Isl6lV5QCHWFh3qZ0JnuYKhWolTjaw==", "e8b6d373-6eae-4b02-b8e7-36c6644c6277" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a494a41e-4f2f-48b6-a7a4-afd48f954ba6", "AQAAAAIAAYagAAAAELWxxgiBkGNprB+wah6b9JN/XXQ5ULpsfW2N1/jclkv5qZwr3LdhZb4sRMdk+C6xBQ==", "2136260d-c218-4539-a455-fdc8657a85ff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efe6e01f-f21b-4e71-80b2-2f3979d2fd0e", "AQAAAAIAAYagAAAAEC2n40vUm0+jN4MIwKe+RPAOWe7dGzFMXMdZBwHKJMWSmRnpQV6cbYmUzeRea8H9HQ==", "761f7c02-6e25-4e0e-931f-b30b209b430b" });

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 1,
                column: "DrawDate",
                value: new DateTime(2024, 5, 18, 16, 15, 26, 635, DateTimeKind.Local).AddTicks(1851));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 2,
                column: "DrawDate",
                value: new DateTime(2024, 5, 18, 16, 15, 26, 635, DateTimeKind.Local).AddTicks(1904));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 3,
                column: "DrawDate",
                value: new DateTime(2024, 5, 18, 16, 15, 26, 635, DateTimeKind.Local).AddTicks(1906));

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 1,
                column: "Prize",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 2,
                column: "Prize",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 3,
                column: "Prize",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prize",
                table: "SearchHistories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fd6795b-d448-46bc-9f33-28f160c29fdd", "AQAAAAIAAYagAAAAENyyHO4FLRsQVBFDbQhTQjKSU5gYkyyh96eIGoKUgOCPcJWt8ZZsIMfZ7ZfwlmyQPA==", "9a07b491-4e09-447c-a8ea-273b45e80ab4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db448e53-d8a5-40f9-8730-cb55fcb7ee45", "AQAAAAIAAYagAAAAEFoA1fYXsRZyvCq476MOOEVHPgBT5Vlfpo3mpEJdlQPKsa6BqUAuSPomIluRdkRzuA==", "183a06d6-78c1-4354-8152-671a8e8d2ca5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7250f19f-c677-49c9-9cdf-bab5e9999e9e", "AQAAAAIAAYagAAAAEIWIi9q9piEON1J+ePOfMFBcKVU7MWzXxbcKxnC74K9gMAxJnt43blKIuJtojX04Ww==", "7f19eacd-bcb7-444f-93c6-739ee4289948" });

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 1,
                column: "DrawDate",
                value: new DateTime(2024, 5, 13, 22, 51, 50, 929, DateTimeKind.Local).AddTicks(5838));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 2,
                column: "DrawDate",
                value: new DateTime(2024, 5, 13, 22, 51, 50, 929, DateTimeKind.Local).AddTicks(5858));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 3,
                column: "DrawDate",
                value: new DateTime(2024, 5, 13, 22, 51, 50, 929, DateTimeKind.Local).AddTicks(5860));
        }
    }
}
