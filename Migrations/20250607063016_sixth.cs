using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Employees_EmpId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_EmpId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Logins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmpId",
                table: "Logins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Logins_EmpId",
                table: "Logins",
                column: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Employees_EmpId",
                table: "Logins",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
