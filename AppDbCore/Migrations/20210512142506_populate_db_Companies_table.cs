using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbCore.Migrations
{
    public partial class populate_db_Companies_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Companies](Name,CreatedDate,LastModifiedDate,IsActive,IsDeleted)
                                    VALUES('Benivo', GETDATE(), null, 1, 1)
                                    , ('DataArt', GETDATE(), null, 1, 1)
                                    , ('Acba', GETDATE(), null, 1, 1)
                                    , ('PicsArt', GETDATE(), null, 1, 1)
                                    , ('Digitain', GETDATE(), null, 1, 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE * FROM [dbo].[Companies]");
        }
    }
}
