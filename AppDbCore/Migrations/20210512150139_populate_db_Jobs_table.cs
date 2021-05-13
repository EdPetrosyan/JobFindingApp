using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbCore.Migrations
{
    public partial class populate_db_Jobs_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO dbo.Jobs(Title,Description,Location,CategoryId,CreatedDate,LastModifiedDate,IsActive,IsDeleted,CompanyId,IsBookmarked,JobTypeId)
VALUES('.NET developer','Digitain is looking for Mid/Senior C# developers with a strong knowledge of .NET or .NET Core programming and database development concepts. He/She must be oriented strongly towards creating and designing cutting-edge web products and will be responsible for delivering large and complex features for our IGaming products. Here you will be involved in the life cycle of development process.','Yerevan, Armenia',1,GetDate(),null,1,0,5,0,1),
('Java Back-End Developer','We are looking for an experienced Java Back-end developer to join our IT team. You will be responsible for the server-side of our web applications. If you have excellent programming skills and a passion for developing applications or improving existing ones, we would like to meet you.','Yerevan, Armenia',1,GetDate(),null,1,0,3,0,1),
('Senior Back End Developer (C#/.NET Core)','Benivo is currently looking for a  Mid to Senior Back End Developer with at least 4 years of relevant experience, who is passionate about designing cutting-edge web products. We are looking for talented professionals who want to grow and challenge themselves, who enjoy working creatively and collaboratively.','Yerevan, Armenia',1,GetDate(),null,1,0,1,0,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE * FROM dbo.Jobs");

        }
    }
}
