using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbCore.Migrations
{
    public partial class populate_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[JobTypes](Type)
                                    VALUES('Full Time'),('Part Time'),('Contractor'),('Intern'),('Seasonal/Temp')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE * FROM [dbo].[JobTypes]");
        }
    }
}
