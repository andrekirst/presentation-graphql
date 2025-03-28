using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ParkplatzDresden.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identifier = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Website = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identifier = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Trend = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Total = table.Column<int>(type: "integer", nullable: true),
                    Free = table.Column<int>(type: "integer", nullable: true),
                    ParkingStateId = table.Column<int>(type: "integer", nullable: true),
                    Address_Street = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Address_Number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Address_PostalCode = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Address_City = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Address_Location_Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Address_Location_Longitude = table.Column<double>(type: "double precision", nullable: true),
                    ServiceTime_IsAllDayOpen = table.Column<bool>(type: "boolean", nullable: true),
                    ServiceTime_Opening = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ServiceTime_Closing = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    OperatorId = table.Column<int>(type: "integer", nullable: true),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkAreas_Operator_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParkAreas_ParkingState_ParkingStateId",
                        column: x => x.ParkingStateId,
                        principalTable: "ParkingState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParkAreas_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSlotsHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Total = table.Column<int>(type: "integer", nullable: true),
                    Free = table.Column<int>(type: "integer", nullable: true),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSlotsHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSlotsHistories_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkAreas_OperatorId",
                table: "ParkAreas",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkAreas_ParkingStateId",
                table: "ParkAreas",
                column: "ParkingStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkAreas_RegionId",
                table: "ParkAreas",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlotsHistories_ParkAreaId",
                table: "ParkingSlotsHistories",
                column: "ParkAreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSlotsHistories");

            migrationBuilder.DropTable(
                name: "ParkAreas");

            migrationBuilder.DropTable(
                name: "Operator");

            migrationBuilder.DropTable(
                name: "ParkingState");

            migrationBuilder.DropTable(
                name: "Region");
        }
    }
}
