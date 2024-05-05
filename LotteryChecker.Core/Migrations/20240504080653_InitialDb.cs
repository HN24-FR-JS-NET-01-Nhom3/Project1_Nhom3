using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseTickets",
                columns: table => new
                {
                    PurchaseTicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LotteryNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTickets", x => x.PurchaseTicketId);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    RewardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RewardValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RewardName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.RewardId);
                });

            migrationBuilder.CreateTable(
                name: "SearchHistories",
                columns: table => new
                {
                    SearchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotteryNumber = table.Column<int>(type: "int", nullable: false),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistories", x => x.SearchId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lotteries",
                columns: table => new
                {
                    LotteryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LotteryNumber = table.Column<int>(type: "int", nullable: false),
                    RewardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotteries", x => x.LotteryId);
                    table.ForeignKey(
                        name: "FK_Lotteries_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "RewardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), null, "Admin", "ADMIN" },
                    { new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"), 0, "34f42146-9119-4f22-b501-79b6f0c70bea", "admin@gmail.com", true, "Hoang Chi", "Hieu", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEObZWxNG1r8UZeN5UtnuVV0m8cnqzpmS03D7q4v5PmDhLqgrYNzxO/RkInKPNgpEcQ==", null, false, "9030bb00-9429-44f8-bdad-0b411089e113", false, "admin@gmail.com" },
                    { new Guid("36b35306-154c-4518-8fc1-d7e756522111"), 0, "afe10563-9256-4561-a53a-d2ceed9f02b5", "vietlq@gmail.com", true, "Le Quang", "Viet", false, null, "VIETLQ@GMAIL.COM", "VIETLQ@GMAIL.COM", "AQAAAAIAAYagAAAAEOGurqtrCr8+LcxC1T4Wl09zH5XyWOD1q3YA474U2uZ18YFWtFJI6gLpTHxwBoW0Lg==", null, false, "ed9d7a80-5a4b-43df-9e2a-5128d0bdeffd", false, "vietlq@gmail.com" },
                    { new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"), 0, "d6e8ebde-1bc0-4164-a6bc-2976010b6e4c", "hieuhv@gmail.com", true, "Ho Van", "Hieu", false, null, "HIEUHV@GMAIL.COM", "HIEUHV@GMAIL.COM", "AQAAAAIAAYagAAAAEE80fOihAt/JJtnr9wxAiuOQJhMRayPOlWbSmYRMXXnkbI7xZxi0yUIH6kEJ79/YnA==", null, false, "2849a7c2-c0f7-4a36-af0b-5bbd906ece59", false, "hieuhv@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseTickets",
                columns: new[] { "PurchaseTicketId", "LotteryNumber", "PurchaseDate", "UserId" },
                values: new object[,]
                {
                    { 1, 123456, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36b35306-154c-4518-8fc1-d7e756522111") },
                    { 2, 234567, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") },
                    { 3, 345678, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") },
                    { 4, 456789, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36b35306-154c-4518-8fc1-d7e756522111") }
                });

            migrationBuilder.InsertData(
                table: "Rewards",
                columns: new[] { "RewardId", "RewardName", "RewardValue" },
                values: new object[,]
                {
                    { 1, "Prize 8", 1000000m },
                    { 2, "Prize 7", 2000000m },
                    { 3, "Prize 6", 3000000m },
                    { 4, "Prize 5", 4000000m },
                    { 5, "Prize 4", 5000000m },
                    { 6, "Prize 3", 6000000m },
                    { 7, "Prize 2", 7000000m },
                    { 8, "Prize 1", 8000000m }
                });

            migrationBuilder.InsertData(
                table: "SearchHistories",
                columns: new[] { "SearchId", "LotteryNumber", "SearchDate", "UserId" },
                values: new object[,]
                {
                    { 1, 123456, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36b35306-154c-4518-8fc1-d7e756522111") },
                    { 2, 234567, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") },
                    { 3, 345678, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f") },
                    { new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"), new Guid("36b35306-154c-4518-8fc1-d7e756522111") },
                    { new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") }
                });

            migrationBuilder.InsertData(
                table: "Lotteries",
                columns: new[] { "LotteryId", "DueDate", "LotteryNumber", "PublishDate", "RewardId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 123456, null, 1 },
                    { 2, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 234567, null, 2 },
                    { 3, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 345678, null, 3 },
                    { 4, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 456789, null, 4 },
                    { 5, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 567890, null, 5 },
                    { 6, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 678901, null, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Lotteries_RewardId",
                table: "Lotteries",
                column: "RewardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Lotteries");

            migrationBuilder.DropTable(
                name: "PurchaseTickets");

            migrationBuilder.DropTable(
                name: "SearchHistories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Rewards");
        }
    }
}
