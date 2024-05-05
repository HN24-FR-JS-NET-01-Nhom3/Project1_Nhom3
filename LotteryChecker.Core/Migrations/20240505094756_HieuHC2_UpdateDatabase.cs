using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class HieuHC2_UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SearchId",
                table: "SearchHistories",
                newName: "SearchHistoryId");

            migrationBuilder.AlterColumn<int>(
                name: "RewardValue",
                table: "Rewards",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "RewardName",
                table: "Rewards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Lotteries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Lotteries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "IsActive", "LastLogin", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1bbf1d87-8df5-4cd3-9f80-819c0bc56ecb", false, null, "AQAAAAIAAYagAAAAEH8MZ1uBFMOYeDfw8I1KJEoUBARzveFKntUP/E5fnLsRBB7406yNd6/tLuShPkLCRA==", "5a03649c-a394-4e04-a0b3-ce4e10a6fda7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "IsActive", "LastLogin", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5aeab1e7-ad8e-42a3-9845-2a83d689d2eb", false, null, "AQAAAAIAAYagAAAAEBfUVENWKnp/dY/gsWg9sCM1FE1OHhoxentFRd13AeXzGRleALVXXlsE1U2TPhriag==", "2c85dd44-81f9-480b-8ae1-2880feae15c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "IsActive", "LastLogin", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1c601fa-e65e-49a5-9270-68bf42b76b9d", false, null, "AQAAAAIAAYagAAAAEAB3JJFF9C5+2QWoom/4wGUF9m/MYxvGCx5gOYXd+eAgdPurVwI16TXFDrXESc1+mg==", "673541d2-fd19-4c65-86c5-ac325bd806fa" });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 1,
                columns: new[] { "Company", "IsPublished" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 2,
                columns: new[] { "Company", "IsPublished" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 3,
                columns: new[] { "Company", "IsPublished" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 4,
                columns: new[] { "Company", "IsPublished" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 5,
                columns: new[] { "Company", "IsPublished" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Lotteries",
                keyColumn: "LotteryId",
                keyValue: 6,
                columns: new[] { "Company", "IsPublished" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 1,
                column: "RewardValue",
                value: 1000000);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 2,
                column: "RewardValue",
                value: 2000000);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 3,
                column: "RewardValue",
                value: 3000000);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 4,
                column: "RewardValue",
                value: 4000000);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 5,
                column: "RewardValue",
                value: 5000000);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 6,
                column: "RewardValue",
                value: 6000000);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 7,
                column: "RewardValue",
                value: 7000000);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 8,
                column: "RewardValue",
                value: 8000000);

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistories_UserId",
                table: "SearchHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTickets_UserId",
                table: "PurchaseTickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseTickets_AspNetUsers_UserId",
                table: "PurchaseTickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchHistories_AspNetUsers_UserId",
                table: "SearchHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseTickets_AspNetUsers_UserId",
                table: "PurchaseTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchHistories_AspNetUsers_UserId",
                table: "SearchHistories");

            migrationBuilder.DropIndex(
                name: "IX_SearchHistories_UserId",
                table: "SearchHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseTickets_UserId",
                table: "PurchaseTickets");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Lotteries");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Lotteries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SearchHistoryId",
                table: "SearchHistories",
                newName: "SearchId");

            migrationBuilder.AlterColumn<decimal>(
                name: "RewardValue",
                table: "Rewards",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RewardName",
                table: "Rewards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
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
                values: new object[] { "34f42146-9119-4f22-b501-79b6f0c70bea", "AQAAAAIAAYagAAAAEObZWxNG1r8UZeN5UtnuVV0m8cnqzpmS03D7q4v5PmDhLqgrYNzxO/RkInKPNgpEcQ==", "9030bb00-9429-44f8-bdad-0b411089e113" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afe10563-9256-4561-a53a-d2ceed9f02b5", "AQAAAAIAAYagAAAAEOGurqtrCr8+LcxC1T4Wl09zH5XyWOD1q3YA474U2uZ18YFWtFJI6gLpTHxwBoW0Lg==", "ed9d7a80-5a4b-43df-9e2a-5128d0bdeffd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6e8ebde-1bc0-4164-a6bc-2976010b6e4c", "AQAAAAIAAYagAAAAEE80fOihAt/JJtnr9wxAiuOQJhMRayPOlWbSmYRMXXnkbI7xZxi0yUIH6kEJ79/YnA==", "2849a7c2-c0f7-4a36-af0b-5bbd906ece59" });

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 1,
                column: "RewardValue",
                value: 1000000m);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 2,
                column: "RewardValue",
                value: 2000000m);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 3,
                column: "RewardValue",
                value: 3000000m);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 4,
                column: "RewardValue",
                value: 4000000m);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 5,
                column: "RewardValue",
                value: 5000000m);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 6,
                column: "RewardValue",
                value: 6000000m);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 7,
                column: "RewardValue",
                value: 7000000m);

            migrationBuilder.UpdateData(
                table: "Rewards",
                keyColumn: "RewardId",
                keyValue: 8,
                column: "RewardValue",
                value: 8000000m);
        }
    }
}
