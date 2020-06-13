using Microsoft.EntityFrameworkCore.Migrations;

namespace Product.Api.Migrations
{
    public partial class seedproductsinprodApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImgUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "beatufil picture by:", "https://i.picsum.photos/id/1021/200/300.jpg", "Photo1", 249.5, 0 },
                    { 2, "beatufil picture by:", "https://i.picsum.photos/id/200/200/300.jpg", "Photo2", 249.5, 0 },
                    { 3, "beatufil picture by:", "https://i.picsum.photos/id/1028/200/300.jpg", "Photo3", 179.5, 0 },
                    { 4, "beatufil picture by:", "https://i.picsum.photos/id/240/200/300.jpg", "Photo4", 349.0, 0 },
                    { 5, "beatufil picture by:", "https://i.picsum.photos/id/104/200/300.jpg", "Photo5", 129.5, 0 },
                    { 6, "beatufil picture by:", "https://i.picsum.photos/id/1045/200/300.jpg", "Photo6", 349.5, 0 },
                    { 7, "beatufil picture by:", "https://i.picsum.photos/id/1061/200/300.jpg", "Photo7", 129.0, 0 },
                    { 8, "beatufil picture by:", "https://i.picsum.photos/id/108/200/300.jpg", "Photo8", 349.0, 0 },
                    { 9, "beatufil picture by:", "https://i.picsum.photos/id/134/200/300.jpg", "Photo9", 99.5, 0 },
                    { 10, "beatufil picture by:", "https://i.picsum.photos/id/145/200/300.jpg", "Photo10", 149.0, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
