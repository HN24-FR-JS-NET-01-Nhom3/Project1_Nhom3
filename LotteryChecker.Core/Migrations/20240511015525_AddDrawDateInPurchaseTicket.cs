using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDrawDateInPurchaseTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d93206a5-579b-4c71-89df-c883db0064c4", "AQAAAAIAAYagAAAAEKrNMMpWhkpyzWXvmE1MW9L8x3uZF2X43k3ZGbVRjYpXVe/Ratf2vMkBJz05XnRe4A==", "792cdfea-5063-459b-a846-0353ea5d5303" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7390ec4a-5f4f-4b9f-925d-e0b8cf179ed1", "AQAAAAIAAYagAAAAEIuVAOqpUWTzy70r0uDDypIqc+VC0nWxXrxdPXvew4BlVpl7Pjpl2zennc+ScKMJgA==", "63ec63f1-cb4e-4bfe-9bda-2e81e3492f87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83fdbd29-0699-42af-9b55-9a855c71689d", "AQAAAAIAAYagAAAAEH449eTqS+O8OWaTiBhdJ+Pl1KW/fCtZ+kaGpDAmbrPzD4JScx2MHQYCiUg2K0ZvEQ==", "ee0bc32a-9ea2-48c6-82a9-369cb7f18f80" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f2a24c6-575d-41b1-8795-6b9a94020a23", "AQAAAAIAAYagAAAAELPfImX4GmoEluZZ3U86wpbQoRsiVYjINJ6mto2sHx55hWdfEmakEKAKYB6R0HLCLA==", "8c7f8d80-03ec-4188-9dbf-c55ae91e6037" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2d05ae2-1fd1-4a9f-8b12-df8457d0715c", "AQAAAAIAAYagAAAAEJoJxNwk2WZcCmg3xr+6+MpbW9kEnVVlCPTejJZ5MwDk8dJMfS4bTcC17D+o8Id+hw==", "80a08b63-aa04-46e4-afd1-69a445823d6c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5345afcd-a31c-49bb-9b2a-9bfbdd534b22", "AQAAAAIAAYagAAAAEKjmUlvC13qQAP9dPH2Dn8F4DhE6FosrFFtMzByIKJCxPNBZKCUlxuddDooBaaolxw==", "b120b898-0b73-4adb-9470-c2811224270c" });
        }
    }
}
