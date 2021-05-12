using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbCore.Migrations
{
    public partial class added_job_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobTypeId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs",
                column: "JobTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "JobType");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                table: "Jobs");
        }
    }
}
