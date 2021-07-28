using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddIsShowCollum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "DocumentUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 20, 9, 48, 0, 497, DateTimeKind.Local).AddTicks(8191),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 19, 12, 44, 17, 587, DateTimeKind.Local).AddTicks(9559));

            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("76dc6e94-6b8c-4468-8452-88456b1ab5f5"),
                column: "ConcurrencyStamp",
                value: "c6b545eb-d5a6-4323-a4e4-6b4c31ff1283");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "baa51ea0-dc08-4f7d-bec5-ef4d91cb1592");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b3ee9c63-9cc2-44e5-b784-1d6e118e877a", "AQAAAAEAACcQAAAAENas8dZqLIExGyPg1IVL5g5+dS5ZrhLthAKx8T5L/Ec+og1Z8+CXSDOExV5I7qD4sA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b06f9fd-e0c8-47e3-a2c6-acb7a307de7d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bf629b0a-42b1-4ff8-a1b4-f61068e466d3", "AQAAAAEAACcQAAAAEM6iiYO+Zh1XTNM7jM3WkGM+aafaqKhL3DnW/tuA6bGolWCU6cjOVIKzh2tO+Jec5w==" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 20, 9, 48, 0, 503, DateTimeKind.Local).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 7, 20, 9, 48, 0, 503, DateTimeKind.Local).AddTicks(9192));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "Documents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "DocumentUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 19, 12, 44, 17, 587, DateTimeKind.Local).AddTicks(9559),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 20, 9, 48, 0, 497, DateTimeKind.Local).AddTicks(8191));

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
        }
    }
}
