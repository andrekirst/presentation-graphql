using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ParkplatzDresden.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class rename_parkingslots_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSlotEntity");

            migrationBuilder.CreateTable(
                name: "ParkingSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Total = table.Column<int>(type: "integer", nullable: true),
                    Free = table.Column<int>(type: "integer", nullable: true),
                    Used = table.Column<int>(type: "integer", nullable: true),
                    ParkAreaEntityId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSlots_ParkAreas_ParkAreaEntityId",
                        column: x => x.ParkAreaEntityId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlots_ParkAreaEntityId",
                table: "ParkingSlots",
                column: "ParkAreaEntityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSlots");

            migrationBuilder.CreateTable(
                name: "ParkingSlotEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParkAreaEntityId = table.Column<int>(type: "integer", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Free = table.Column<int>(type: "integer", nullable: true),
                    Total = table.Column<int>(type: "integer", nullable: true),
                    Used = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSlotEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSlotEntity_ParkAreas_ParkAreaEntityId",
                        column: x => x.ParkAreaEntityId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlotEntity_ParkAreaEntityId",
                table: "ParkingSlotEntity",
                column: "ParkAreaEntityId",
                unique: true);
        }
    }
}
