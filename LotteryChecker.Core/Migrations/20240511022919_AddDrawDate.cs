using System;
using LotteryChecker.Core.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDrawDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                "DrawDate", 
                "PurchaseTickets", 
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);
            
            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 5,
                column: "DrawDate",
                value: new DateTime(2024, 5, 11, 9, 29, 19, 260, DateTimeKind.Local).AddTicks(2597));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 6,
                column: "DrawDate",
                value: new DateTime(2024, 5, 11, 9, 29, 19, 260, DateTimeKind.Local).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 7,
                column: "DrawDate",
                value: new DateTime(2024, 5, 11, 9, 29, 19, 260, DateTimeKind.Local).AddTicks(2748));
            
            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 8,
                column: "DrawDate",
                value: new DateTime(2024, 5, 11, 9, 29, 19, 260, DateTimeKind.Local).AddTicks(2748));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9669853b-c91a-4c59-8543-72f78080d512", "AQAAAAIAAYagAAAAEMUw8GHz9Dvz2bcQRc3YgRplZ9dAJwJQf+8C8QAnkL35CNoeQMQ0IA53dFN33BPEnw==", "d49e0eb8-1657-460a-9821-612d89bfbea3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27b76c40-3b6f-4810-9119-ac2dba06afaa", "AQAAAAIAAYagAAAAEHHyHp4klJxkkSx15HIZjyuHPb/6PTW5qmqcebCqrmZiHAor9C1xWL30GntysNy19A==", "708ac3dd-5654-46d9-80fe-702a8c2abe09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3da04b7a-3f62-44e0-86b4-64a12a7259b2", "AQAAAAIAAYagAAAAEOIketJ53bRDHTEfBa9mKJl2qAvuD22dQTQuzWzJP4VpBo0sB2zPIYhN06HJC8tdQw==", "d37c1c5e-8ef1-433f-91f6-369331b60279" });

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 5,
                column: "DrawDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 6,
                column: "DrawDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 7,
                column: "DrawDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            
            migrationBuilder.UpdateData(
                table: "PurchaseTickets",
                keyColumn: "PurchaseTicketId",
                keyValue: 8,
                column: "DrawDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
