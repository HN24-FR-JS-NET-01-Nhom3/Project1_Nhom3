using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDrawDateForSearchHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DrawDate",
                table: "SearchHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 1,
                column: "DrawDate",
                value: new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 2,
                column: "DrawDate",
                value: new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SearchHistories",
                keyColumn: "SearchHistoryId",
                keyValue: 3,
                column: "DrawDate",
                value: new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrawDate",
                table: "SearchHistories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e642427-e5bc-46fb-9282-5d8229bce2c1", "AQAAAAIAAYagAAAAEDlC8KWx/u0HSd8QP8QtpixhvJvupaZEfFun5TAjK4asuqBLo+eY30zNfcuJniaOsQ==", "277e9a29-f5d5-4106-962b-cdef9ee9fca3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1be48b03-ac2d-45fa-8dd4-a99ff265e70a", "AQAAAAIAAYagAAAAEMPibgRM4/Os7HM7jSeYsj977OLhg8vIJ4yUG3ZkqN+pXim+yjMWAIsVwgDLfRG1yQ==", "74d778ac-5f44-4743-9473-61fc967a131e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02c1092c-ed7a-4943-8dbb-31cd82d42946", "AQAAAAIAAYagAAAAEPSE8jCNVNBOn9OVLmZPVVbXwS2milZt1t0Uia94kQkj0lmAiRTb+cBF4J6QHuaHkg==", "1b976011-d3a3-46ad-bd81-5bd42e284711" });

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 1,
                column: "DrawDate",
                value: new DateTime(2024, 5, 11, 9, 29, 19, 260, DateTimeKind.Local).AddTicks(2597));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 2,
                column: "DrawDate",
                value: new DateTime(2024, 5, 11, 9, 29, 19, 260, DateTimeKind.Local).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 3,
                column: "DrawDate",
                value: new DateTime(2024, 5, 11, 9, 29, 19, 260, DateTimeKind.Local).AddTicks(2748));
        }
    }
}
