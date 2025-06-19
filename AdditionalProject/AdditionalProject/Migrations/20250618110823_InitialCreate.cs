using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdditionalProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    IdEvent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.IdEvent);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    IdParticipant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.IdParticipant);
                });

            migrationBuilder.CreateTable(
                name: "Speaker",
                columns: table => new
                {
                    IdSpeaker = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaker", x => x.IdSpeaker);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistration",
                columns: table => new
                {
                    IdEvent = table.Column<int>(type: "int", nullable: false),
                    IdParticipant = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistration", x => new { x.IdEvent, x.IdParticipant });
                    table.ForeignKey(
                        name: "FK_EventRegistration_Event_IdEvent",
                        column: x => x.IdEvent,
                        principalTable: "Event",
                        principalColumn: "IdEvent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRegistration_Participant_IdParticipant",
                        column: x => x.IdParticipant,
                        principalTable: "Participant",
                        principalColumn: "IdParticipant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSpeaker",
                columns: table => new
                {
                    IdSpeaker = table.Column<int>(type: "int", nullable: false),
                    IdEvent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpeaker", x => new { x.IdEvent, x.IdSpeaker });
                    table.ForeignKey(
                        name: "FK_EventSpeaker_Event_IdEvent",
                        column: x => x.IdEvent,
                        principalTable: "Event",
                        principalColumn: "IdEvent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSpeaker_Speaker_IdSpeaker",
                        column: x => x.IdSpeaker,
                        principalTable: "Speaker",
                        principalColumn: "IdSpeaker",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "IdEvent", "Capacity", "Date", "Name" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2025, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Konferencja Ogórek" },
                    { 2, 50, new DateTime(2025, 7, 16, 13, 0, 0, 0, DateTimeKind.Unspecified), "AI Workshop" },
                    { 3, 75, new DateTime(2025, 7, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Kochamy Pomidory" }
                });

            migrationBuilder.InsertData(
                table: "Participant",
                columns: new[] { "IdParticipant", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "kasia.w@xd.com", "Katarzyna", "Wójcik" },
                    { 2, "piotr.l@xdxd.com", "Piotr", "Lewandowski" },
                    { 3, "zofia.k@xdd.com", "Zofia", "Kaczmarek" }
                });

            migrationBuilder.InsertData(
                table: "Speaker",
                columns: new[] { "IdSpeaker", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "anna.nowak@gmail.com", "Anna", "Nowak" },
                    { 2, "jan.kowalski@wp.com", "Jan", "Kowalski" },
                    { 3, "ewa.w@yahoo.com", "Ewa", "Wiśniewska" }
                });

            migrationBuilder.InsertData(
                table: "EventRegistration",
                columns: new[] { "IdEvent", "IdParticipant", "RegisteredAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, new DateTime(2025, 6, 2, 12, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, new DateTime(2025, 6, 5, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2025, 6, 8, 9, 45, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "EventSpeaker",
                columns: new[] { "IdEvent", "IdSpeaker" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_IdParticipant",
                table: "EventRegistration",
                column: "IdParticipant");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpeaker_IdSpeaker",
                table: "EventSpeaker",
                column: "IdSpeaker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRegistration");

            migrationBuilder.DropTable(
                name: "EventSpeaker");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Speaker");
        }
    }
}
