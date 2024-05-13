using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
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
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                name: "Rewards",
                columns: table => new
                {
                    RewardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RewardValue = table.Column<int>(type: "int", nullable: false),
                    RewardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.RewardId);
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
                name: "PurchaseTickets",
                columns: table => new
                {
                    PurchaseTicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LotteryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrawDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTickets", x => x.PurchaseTicketId);
                    table.ForeignKey(
                        name: "FK_PurchaseTickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "SearchHistories",
                columns: table => new
                {
                    SearchHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotteryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrawDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistories", x => x.SearchHistoryId);
                    table.ForeignKey(
                        name: "FK_SearchHistories_AspNetUsers_UserId",
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
                    DrawDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LotteryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastLogin", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"), 0, "2fd6795b-d448-46bc-9f33-28f160c29fdd", "admin@gmail.com", true, "Hoang Chi", false, null, "Hieu", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAENyyHO4FLRsQVBFDbQhTQjKSU5gYkyyh96eIGoKUgOCPcJWt8ZZsIMfZ7ZfwlmyQPA==", null, false, "9a07b491-4e09-447c-a8ea-273b45e80ab4", false, "admin@gmail.com" },
                    { new Guid("36b35306-154c-4518-8fc1-d7e756522111"), 0, "db448e53-d8a5-40f9-8730-cb55fcb7ee45", "vietlq@gmail.com", true, "Le Quang", false, null, "Viet", false, null, "VIETLQ@GMAIL.COM", "VIETLQ@GMAIL.COM", "AQAAAAIAAYagAAAAEFoA1fYXsRZyvCq476MOOEVHPgBT5Vlfpo3mpEJdlQPKsa6BqUAuSPomIluRdkRzuA==", null, false, "183a06d6-78c1-4354-8152-671a8e8d2ca5", false, "vietlq@gmail.com" },
                    { new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"), 0, "7250f19f-c677-49c9-9cdf-bab5e9999e9e", "hieuhv@gmail.com", true, "Ho Van", false, null, "Hieu", false, null, "HIEUHV@GMAIL.COM", "HIEUHV@GMAIL.COM", "AQAAAAIAAYagAAAAEIWIi9q9piEON1J+ePOfMFBcKVU7MWzXxbcKxnC74K9gMAxJnt43blKIuJtojX04Ww==", null, false, "7f19eacd-bcb7-444f-93c6-739ee4289948", false, "hieuhv@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Rewards",
                columns: new[] { "RewardId", "RewardName", "RewardValue" },
                values: new object[,]
                {
                    { 1, "Prize 8", 1000000 },
                    { 2, "Prize 7", 2000000 },
                    { 3, "Prize 6", 3000000 },
                    { 4, "Prize 5", 4000000 },
                    { 5, "Prize 4", 5000000 },
                    { 6, "Prize 3", 6000000 },
                    { 7, "Prize 2", 7000000 },
                    { 8, "Prize 1", 8000000 }
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
                columns: new[] { "LotteryId", "Company", "DrawDate", "IsPublished", "LotteryNumber", "PublishDate", "RewardId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "123456", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, null, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "234567", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, null, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "345678", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, null, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "456789", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, null, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "567890", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, null, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "678901", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseTickets",
                columns: new[] { "PurchaseTicketId", "DrawDate", "LotteryNumber", "PurchaseDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 13, 22, 51, 50, 929, DateTimeKind.Local).AddTicks(5838), "123456", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36b35306-154c-4518-8fc1-d7e756522111") },
                    { 2, new DateTime(2024, 5, 13, 22, 51, 50, 929, DateTimeKind.Local).AddTicks(5858), "234567", new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") },
                    { 3, new DateTime(2024, 5, 13, 22, 51, 50, 929, DateTimeKind.Local).AddTicks(5860), "345678", new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "456789", new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36b35306-154c-4518-8fc1-d7e756522111") }
                });

            migrationBuilder.InsertData(
                table: "SearchHistories",
                columns: new[] { "SearchHistoryId", "DrawDate", "LotteryNumber", "SearchDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "123456", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36b35306-154c-4518-8fc1-d7e756522111") },
                    { 2, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "234567", new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") },
                    { 3, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "345678", new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d") }
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

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTickets_UserId",
                table: "PurchaseTickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistories_UserId",
                table: "SearchHistories",
                column: "UserId");
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
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SearchHistories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
