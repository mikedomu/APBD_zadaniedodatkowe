using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdditionalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Event",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 1,
                column: "Description",
                value: "Event poświęcony ogórkom kiszonym i jak je przygotowywać");

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 2,
                column: "Description",
                value: "Warsztaty o AI");

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 3,
                column: "Description",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Event");
        }
    }
}
