using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDomainAndConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompletionNotes",
                table: "WorkOrders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ManufacturerId1",
                table: "Conditioners",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId1",
                table: "Conditioners",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId1",
                table: "Conditioners",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ConditionerId",
                table: "WorkOrders",
                column: "ConditionerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_PriorityId",
                table: "WorkOrders",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_StatusId",
                table: "WorkOrders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceSchedules_ConditionerId",
                table: "MaintenanceSchedules",
                column: "ConditionerId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceSchedules_FrequencyId",
                table: "MaintenanceSchedules",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Conditioners_ManufacturerId1",
                table: "Conditioners",
                column: "ManufacturerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Conditioners_StatusId1",
                table: "Conditioners",
                column: "StatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Conditioners_TypeId1",
                table: "Conditioners",
                column: "TypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditioners_ConditionerStatuses_StatusId1",
                table: "Conditioners",
                column: "StatusId1",
                principalTable: "ConditionerStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditioners_ConditionerTypes_TypeId1",
                table: "Conditioners",
                column: "TypeId1",
                principalTable: "ConditionerTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditioners_Manufacturers_ManufacturerId1",
                table: "Conditioners",
                column: "ManufacturerId1",
                principalTable: "Manufacturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceSchedules_Conditioners_ConditionerId",
                table: "MaintenanceSchedules",
                column: "ConditionerId",
                principalTable: "Conditioners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceSchedules_MaintenanceFrequencies_FrequencyId",
                table: "MaintenanceSchedules",
                column: "FrequencyId",
                principalTable: "MaintenanceFrequencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Conditioners_ConditionerId",
                table: "WorkOrders",
                column: "ConditionerId",
                principalTable: "Conditioners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_WorkOrderPriorities_PriorityId",
                table: "WorkOrders",
                column: "PriorityId",
                principalTable: "WorkOrderPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_StatusId",
                table: "WorkOrders",
                column: "StatusId",
                principalTable: "WorkOrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conditioners_ConditionerStatuses_StatusId1",
                table: "Conditioners");

            migrationBuilder.DropForeignKey(
                name: "FK_Conditioners_ConditionerTypes_TypeId1",
                table: "Conditioners");

            migrationBuilder.DropForeignKey(
                name: "FK_Conditioners_Manufacturers_ManufacturerId1",
                table: "Conditioners");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceSchedules_Conditioners_ConditionerId",
                table: "MaintenanceSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceSchedules_MaintenanceFrequencies_FrequencyId",
                table: "MaintenanceSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Conditioners_ConditionerId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_WorkOrderPriorities_PriorityId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_StatusId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ConditionerId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_PriorityId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_StatusId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceSchedules_ConditionerId",
                table: "MaintenanceSchedules");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceSchedules_FrequencyId",
                table: "MaintenanceSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Conditioners_ManufacturerId1",
                table: "Conditioners");

            migrationBuilder.DropIndex(
                name: "IX_Conditioners_StatusId1",
                table: "Conditioners");

            migrationBuilder.DropIndex(
                name: "IX_Conditioners_TypeId1",
                table: "Conditioners");

            migrationBuilder.DropColumn(
                name: "ManufacturerId1",
                table: "Conditioners");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "Conditioners");

            migrationBuilder.DropColumn(
                name: "TypeId1",
                table: "Conditioners");

            migrationBuilder.AlterColumn<string>(
                name: "CompletionNotes",
                table: "WorkOrders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
