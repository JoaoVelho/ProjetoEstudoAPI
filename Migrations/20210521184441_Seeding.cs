using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoEstudoAPI.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_ArtistId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Albums_AlbumId",
                table: "Musics");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Musics",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "AlbumId",
                table: "Musics",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Albums",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Albums",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "64d94610-25d3-4d82-a0fc-27351ead67de", 0, "28a2a66c-8f50-4e01-9986-6f951bd8b96e", "jcole@email.com", true, true, null, "J Cole", "JCOLE@EMAIL.COM", "JCOLE@EMAIL.COM", "AQAAAAEAACcQAAAAEEf+5gVrIZBhIAorpI/6QtDq2dIGLutY3QAPeOZiunTzzfFCHcIOUEUFbIy5Twut5g==", null, false, "4TTHTLZP6K7R4KWM27OFNZCVOUWCBGAP", false, "jcole@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a2631013-893d-4fbc-baed-e5a24001c907", 0, "5d75c2f8-a019-49b7-ae9f-4355b6e0fa17", "pfloyd@email.com", true, true, null, "Pink Floyd", "PFLOYD@EMAIL.COM", "PFLOYD@EMAIL.COM", "AQAAAAEAACcQAAAAEBiA5KYQ3DEgqjd1skx33Ru0D2WjV5hanIy2NIlqtac6y1mwo8JS7v0nzXbSFlGXRw==", null, false, "UU3ZV2L3GZL24ZNDXZWOA5XDC2QKVJAB", false, "pfloyd@email.com" });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "ArtistId", "Name" },
                values: new object[,]
                {
                    { 1, "64d94610-25d3-4d82-a0fc-27351ead67de", "The Off-Season" },
                    { 2, "64d94610-25d3-4d82-a0fc-27351ead67de", "2014 Forest Hills Drive" },
                    { 3, "a2631013-893d-4fbc-baed-e5a24001c907", "The Dark Side of the Moon" },
                    { 4, "a2631013-893d-4fbc-baed-e5a24001c907", "The Wall" }
                });

            migrationBuilder.InsertData(
                table: "Musics",
                columns: new[] { "Id", "AlbumId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "amari" },
                    { 2, 1, "pride is the devil" },
                    { 3, 2, "No Role Modelz" },
                    { 4, 2, "Note to Self" },
                    { 5, 3, "Breathe" },
                    { 6, 3, "Eclipse" },
                    { 7, 4, "Mother" },
                    { 8, 4, "Hey You" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Albums_AlbumId",
                table: "Musics",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_ArtistId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Albums_AlbumId",
                table: "Musics");

            migrationBuilder.DeleteData(
                table: "Musics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Musics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Musics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Musics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Musics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Musics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Musics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Musics",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64d94610-25d3-4d82-a0fc-27351ead67de");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2631013-893d-4fbc-baed-e5a24001c907");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Musics",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlbumId",
                table: "Musics",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Albums",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Albums",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Albums_AlbumId",
                table: "Musics",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
