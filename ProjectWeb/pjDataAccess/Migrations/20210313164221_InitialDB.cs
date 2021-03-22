using Microsoft.EntityFrameworkCore.Migrations;

namespace pjDataAccess.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Job_ProjectId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Job");

            migrationBuilder.AddColumn<bool>(
                name: "ProjectStatus",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Job_ProjectId",
                table: "Job",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Job_ProjectId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ProjectStatus",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_ProjectId",
                table: "Job",
                column: "ProjectId",
                unique: true);
        }
    }
}
