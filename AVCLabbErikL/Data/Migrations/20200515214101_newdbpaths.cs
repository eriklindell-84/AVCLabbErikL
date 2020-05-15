using Microsoft.EntityFrameworkCore.Migrations;

namespace AVCLabbErikL.Data.Migrations
{
    public partial class newdbpaths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    OrderDate = table.Column<string>(nullable: false),
                    OrderAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
