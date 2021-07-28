using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPermistion",
                table: "DocumentUsers");

            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "Votes",
                table: "DocumentUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "RequestExtension",
                table: "DocumentUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "DocumentUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 18, 9, 12, 48, 80, DateTimeKind.Local).AddTicks(9808),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Votes",
                table: "DocumentUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "RequestExtension",
                table: "DocumentUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "DocumentUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 18, 9, 12, 48, 80, DateTimeKind.Local).AddTicks(9808));

            migrationBuilder.AddColumn<bool>(
                name: "IsPermistion",
                table: "DocumentUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                value: "7896037b-45ff-4cad-ae17-671bfae89dda");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "126c567e-a185-46e6-82e8-0334cd6d971e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fd2b4f76-85b2-46ad-8180-af201ac0bacf", "AQAAAAEAACcQAAAAEEE7J2uBSRUo4ywjAhoeyYa9tYObBCe6jf4jSz/c41FbkuE7NcYjG26K9gKnj9GWgg==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b06f9fd-e0c8-47e3-a2c6-acb7a307de7d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5879f69b-6594-4491-bbd3-ed66343b0055", "AQAAAAEAACcQAAAAEBH2miHrAR0TuV4Z2cmltEMTJm6JSj97JszwTPUtKo3ARpyMhTMJcmUxAd9+inyHMg==" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "IsShow" },
                values: new object[] { new DateTime(2021, 7, 14, 17, 12, 49, 100, DateTimeKind.Local).AddTicks(9332), true });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "IsShow" },
                values: new object[] { new DateTime(2021, 7, 14, 17, 12, 49, 101, DateTimeKind.Local).AddTicks(8776), true });
        }
    }
}
