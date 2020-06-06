using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkleBitersCreche.Data.Migrations
{
    public partial class AddExtraPropertiesToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Miles",
                table: "ServiceHeader");

            migrationBuilder.AlterColumn<string>(
                name: "PPS",
                table: "Student",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Relationship",
                table: "Student",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "ServiceHeader",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Relationship",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "ServiceHeader");

            migrationBuilder.AlterColumn<string>(
                name: "PPS",
                table: "Student",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 9);

            migrationBuilder.AddColumn<double>(
                name: "Miles",
                table: "ServiceHeader",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
