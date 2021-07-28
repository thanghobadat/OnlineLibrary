using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddMailSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "DocumentUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 19, 12, 44, 17, 587, DateTimeKind.Local).AddTicks(9559),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 18, 9, 12, 48, 80, DateTimeKind.Local).AddTicks(9808));

            migrationBuilder.CreateTable(
                name: "MailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailSettings", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("76dc6e94-6b8c-4468-8452-88456b1ab5f5"),
                column: "ConcurrencyStamp",
                value: "250ab922-1b8a-4425-88c2-57c7773412f2");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1358ddf5-7dc0-4185-bf90-58f78f5f3def");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "981d7969-8c0d-42eb-8c87-1bbd465810ba", "AQAAAAEAACcQAAAAEE3NCaKKUp7Fa5tF4gmdx57h2soAgz578nnPOa7X3Xx6vO/i91KlLa1OCdh83ovIJw==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b06f9fd-e0c8-47e3-a2c6-acb7a307de7d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "718ca83c-a732-49a1-8083-42a2bb1a63c2", "AQAAAAEAACcQAAAAEKZCv3VdmwP1F2OKxxt2PYUqEI2i13JzrICO6W3AVn3khhwq4sXap8WCW9IJM4sJyQ==" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 19, 12, 44, 17, 594, DateTimeKind.Local).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 7, 19, 12, 44, 17, 594, DateTimeKind.Local).AddTicks(4830));

            migrationBuilder.InsertData(
                table: "MailSettings",
                columns: new[] { "Id", "DisplayName", "EMail", "Host", "Password", "Port" },
                values: new object[] { 1, "hoangthanh01022000@gmail.com", "hoangthanh01022000@gmail.com", "smtp.gmail.com", "1233211234", 587 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailSettings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "DocumentUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 18, 9, 12, 48, 80, DateTimeKind.Local).AddTicks(9808),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 19, 12, 44, 17, 587, DateTimeKind.Local).AddTicks(9559));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("76dc6e94-6b8c-4468-8452-88456b1ab5f5"),
                column: "ConcurrencyStamp",
                value: "441623a9-c0a0-43a2-b141-0513f53df435");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "b9f6261d-e2aa-4a20-9921-6d6328704202");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1cafa0b8-506e-4183-8a60-5f7f33b1657f", "AQAAAAEAACcQAAAAEFe4fRSEtt1JETtWqXwoIXk201bU3J0vw28vHT8mGQ8GMrNcL4yMl1/wziCsAwMIpw==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b06f9fd-e0c8-47e3-a2c6-acb7a307de7d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ed9a006f-5461-4b0a-a941-8fa2ddc7affc", "AQAAAAEAACcQAAAAEGZiiGuHlnJjczezIIJZX6J/SWZ19nMvIngzk8wl0lCVQKK4pMPptMVst4Kn01lR4A==" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 18, 9, 12, 48, 86, DateTimeKind.Local).AddTicks(3495));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 7, 18, 9, 12, 48, 86, DateTimeKind.Local).AddTicks(4501));
        }
    }
}
