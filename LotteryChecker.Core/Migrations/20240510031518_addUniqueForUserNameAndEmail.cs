using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class addUniqueForUserNameAndEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49519764-bd81-439f-ba0b-60e1b0e773b9", "AQAAAAIAAYagAAAAEDmdr43JJanNhyEWcvw2snUGOBTKfsCv1G1jE75FbsvEjtKeVkRqbeHWy4QzybVPIg==", "f8dce32a-5551-4a49-9794-fc2f66261ee6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c5f8e82-c3b1-4076-a9ca-beb6a315cc39", "AQAAAAIAAYagAAAAELDFNx3oyiAHXEtaRC7XFUPh3SEa02BSvsORcZC4AxbSAUqcYMsbfPgasNArnlMeYg==", "71cd294d-e8af-437a-8895-7903d87a6e3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b72ecf2-549e-4110-a4ab-62a720d9b904", "AQAAAAIAAYagAAAAEO12J6gX030BOEN7VknYqvPCfQJHNr3/b7QBX8CdA6UxmCwL9BNOTzGSbkJOzlOtsQ==", "fe312aea-4937-4f83-aaa6-760cea5b1570" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
        }
    }
}
