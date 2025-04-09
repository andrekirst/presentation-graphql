using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiService.Migrations
{
    /// <inheritdoc />
    public partial class add_statistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayStatisticsAggregates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Minimum = table.Column<int>(type: "integer", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    Average = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayStatisticsAggregates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayStatisticsAggregates_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayStatisticsValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayStatisticsValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayStatisticsValues_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HourStatisticsAggregates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Minimum = table.Column<int>(type: "integer", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    Average = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    Hour = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourStatisticsAggregates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourStatisticsAggregates_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HourStatisticsValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    Hour = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourStatisticsValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourStatisticsValues_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthStatisticsAggregateEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Minimum = table.Column<int>(type: "integer", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    Average = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthStatisticsAggregateEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthStatisticsAggregateEntity_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthStatisticsValuesEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthStatisticsValuesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthStatisticsValuesEntity_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YearStatisticsAggregates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Minimum = table.Column<int>(type: "integer", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    Average = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearStatisticsAggregates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearStatisticsAggregates_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YearStatisticsValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkAreaId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearStatisticsValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearStatisticsValues_ParkAreas_ParkAreaId",
                        column: x => x.ParkAreaId,
                        principalTable: "ParkAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayStatisticsAggregates_ParkAreaId",
                table: "DayStatisticsAggregates",
                column: "ParkAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DayStatisticsValues_ParkAreaId",
                table: "DayStatisticsValues",
                column: "ParkAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_HourStatisticsAggregates_ParkAreaId",
                table: "HourStatisticsAggregates",
                column: "ParkAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_HourStatisticsValues_ParkAreaId",
                table: "HourStatisticsValues",
                column: "ParkAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthStatisticsAggregateEntity_ParkAreaId",
                table: "MonthStatisticsAggregateEntity",
                column: "ParkAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthStatisticsValuesEntity_ParkAreaId",
                table: "MonthStatisticsValuesEntity",
                column: "ParkAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_YearStatisticsAggregates_ParkAreaId",
                table: "YearStatisticsAggregates",
                column: "ParkAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_YearStatisticsValues_ParkAreaId",
                table: "YearStatisticsValues",
                column: "ParkAreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayStatisticsAggregates");

            migrationBuilder.DropTable(
                name: "DayStatisticsValues");

            migrationBuilder.DropTable(
                name: "HourStatisticsAggregates");

            migrationBuilder.DropTable(
                name: "HourStatisticsValues");

            migrationBuilder.DropTable(
                name: "MonthStatisticsAggregateEntity");

            migrationBuilder.DropTable(
                name: "MonthStatisticsValuesEntity");

            migrationBuilder.DropTable(
                name: "YearStatisticsAggregates");

            migrationBuilder.DropTable(
                name: "YearStatisticsValues");
        }
    }
}
