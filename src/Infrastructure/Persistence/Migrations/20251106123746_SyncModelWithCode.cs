using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelWithCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Conditioners_ManufacturerId",
                table: "Conditioners",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Conditioners_StatusId",
                table: "Conditioners",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Conditioners_TypeId",
                table: "Conditioners",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditioners_ConditionerStatuses_StatusId",
                table: "Conditioners",
                column: "StatusId",
                principalTable: "ConditionerStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conditioners_ConditionerTypes_TypeId",
                table: "Conditioners",
                column: "TypeId",
                principalTable: "ConditionerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conditioners_Manufacturers_ManufacturerId",
                table: "Conditioners",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conditioners_ConditionerStatuses_StatusId",
                table: "Conditioners");

            migrationBuilder.DropForeignKey(
                name: "FK_Conditioners_ConditionerTypes_TypeId",
                table: "Conditioners");

            migrationBuilder.DropForeignKey(
                name: "FK_Conditioners_Manufacturers_ManufacturerId",
                table: "Conditioners");

            migrationBuilder.DropIndex(
                name: "IX_Conditioners_ManufacturerId",
                table: "Conditioners");

            migrationBuilder.DropIndex(
                name: "IX_Conditioners_StatusId",
                table: "Conditioners");

            migrationBuilder.DropIndex(
                name: "IX_Conditioners_TypeId",
                table: "Conditioners");

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
        }
    }
}
