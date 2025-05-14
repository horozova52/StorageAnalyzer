using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageAnalyzer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalFiles",
                table: "ScanSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TotalSize",
                table: "ScanSessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "ScanSessionId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_ScanSessionId",
                table: "Files",
                column: "ScanSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_ScanSessions_ScanSessionId",
                table: "Files",
                column: "ScanSessionId",
                principalTable: "ScanSessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_ScanSessions_ScanSessionId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_ScanSessionId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "TotalFiles",
                table: "ScanSessions");

            migrationBuilder.DropColumn(
                name: "TotalSize",
                table: "ScanSessions");

            migrationBuilder.DropColumn(
                name: "ScanSessionId",
                table: "Files");
        }
    }
}
