using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HephaestusSQLiteRepo.Migrations
{
    public partial class user_can_see_focus_task_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndTime",
                table: "FocusTasks",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "FocusTasks");
        }
    }
}
