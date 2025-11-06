using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMtoNRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConditionerMaintenanceSchedules",
                columns: table => new
                {
                    ConditionerId = table.Column<Guid>(type: "uuid", nullable: false),
                    MaintenanceScheduleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionerMaintenanceSchedules", x => new { x.ConditionerId, x.MaintenanceScheduleId });
                    table.ForeignKey(
                        name: "fk_conditioner_maintenance_schedule_conditioners_id",
                        column: x => x.ConditionerId,
                        principalTable: "Conditioners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_conditioner_maintenance_schedule_schedules_id",
                        column: x => x.MaintenanceScheduleId,
                        principalTable: "MaintenanceSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConditionerMaintenanceSchedules_MaintenanceScheduleId",
                table: "ConditionerMaintenanceSchedules",
                column: "MaintenanceScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConditionerMaintenanceSchedules");
        }
    }
}
