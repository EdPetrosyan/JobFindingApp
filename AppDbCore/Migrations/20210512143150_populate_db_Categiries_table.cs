using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbCore.Migrations
{
    public partial class populate_db_Categiries_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Categories](Name,CreatedDate,LastModifiedDate,IsActive,IsDeleted)
                                    VALUES('Software development',GETDATE(),null,1,1)
                                    ,('Quality Assurance /Control',GETDATE(),null,1,1)
                                    ,('Web/Graphic design',GETDATE(),null,1,1)
                                    ,('Product/Project Management',GETDATE(),null,1,1)
                                    ,('Hardware design',GETDATE(),null,1,1)
                                    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE * FROM [dbo].[Categories]");
        }
    }
}
