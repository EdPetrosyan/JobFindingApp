using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbCore.Migrations
{
    public partial class populate_db_Make_data_active : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE [Categories] SET IsDeleted = 0
                                    UPDATE [Companies] SET IsDeleted = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
