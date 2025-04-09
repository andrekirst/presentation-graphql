using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiService.Migrations
{
    /// <inheritdoc />
    public partial class rename_month_statistic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonthStatisticsAggregateEntity_ParkAreas_ParkAreaId",
                table: "MonthStatisticsAggregateEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthStatisticsValuesEntity_ParkAreas_ParkAreaId",
                table: "MonthStatisticsValuesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthStatisticsValuesEntity",
                table: "MonthStatisticsValuesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthStatisticsAggregateEntity",
                table: "MonthStatisticsAggregateEntity");

            migrationBuilder.RenameTable(
                name: "MonthStatisticsValuesEntity",
                newName: "MonthStatisticsValues");

            migrationBuilder.RenameTable(
                name: "MonthStatisticsAggregateEntity",
                newName: "MonthStatisticsAggregates");

            migrationBuilder.RenameIndex(
                name: "IX_MonthStatisticsValuesEntity_ParkAreaId",
                table: "MonthStatisticsValues",
                newName: "IX_MonthStatisticsValues_ParkAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_MonthStatisticsAggregateEntity_ParkAreaId",
                table: "MonthStatisticsAggregates",
                newName: "IX_MonthStatisticsAggregates_ParkAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthStatisticsValues",
                table: "MonthStatisticsValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthStatisticsAggregates",
                table: "MonthStatisticsAggregates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthStatisticsAggregates_ParkAreas_ParkAreaId",
                table: "MonthStatisticsAggregates",
                column: "ParkAreaId",
                principalTable: "ParkAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonthStatisticsValues_ParkAreas_ParkAreaId",
                table: "MonthStatisticsValues",
                column: "ParkAreaId",
                principalTable: "ParkAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonthStatisticsAggregates_ParkAreas_ParkAreaId",
                table: "MonthStatisticsAggregates");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthStatisticsValues_ParkAreas_ParkAreaId",
                table: "MonthStatisticsValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthStatisticsValues",
                table: "MonthStatisticsValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthStatisticsAggregates",
                table: "MonthStatisticsAggregates");

            migrationBuilder.RenameTable(
                name: "MonthStatisticsValues",
                newName: "MonthStatisticsValuesEntity");

            migrationBuilder.RenameTable(
                name: "MonthStatisticsAggregates",
                newName: "MonthStatisticsAggregateEntity");

            migrationBuilder.RenameIndex(
                name: "IX_MonthStatisticsValues_ParkAreaId",
                table: "MonthStatisticsValuesEntity",
                newName: "IX_MonthStatisticsValuesEntity_ParkAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_MonthStatisticsAggregates_ParkAreaId",
                table: "MonthStatisticsAggregateEntity",
                newName: "IX_MonthStatisticsAggregateEntity_ParkAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthStatisticsValuesEntity",
                table: "MonthStatisticsValuesEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthStatisticsAggregateEntity",
                table: "MonthStatisticsAggregateEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthStatisticsAggregateEntity_ParkAreas_ParkAreaId",
                table: "MonthStatisticsAggregateEntity",
                column: "ParkAreaId",
                principalTable: "ParkAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonthStatisticsValuesEntity_ParkAreas_ParkAreaId",
                table: "MonthStatisticsValuesEntity",
                column: "ParkAreaId",
                principalTable: "ParkAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
