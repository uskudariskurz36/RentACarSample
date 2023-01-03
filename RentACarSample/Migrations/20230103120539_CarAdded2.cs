using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACarSample.Migrations
{
    public partial class CarAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Brands_BrandId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_SubBrands_SubBrandId",
                table: "Car");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_Car_SubBrandId",
                table: "Cars",
                newName: "IX_Cars_SubBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_BrandId",
                table: "Cars",
                newName: "IX_Cars_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_SubBrands_SubBrandId",
                table: "Cars",
                column: "SubBrandId",
                principalTable: "SubBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_SubBrands_SubBrandId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_SubBrandId",
                table: "Car",
                newName: "IX_Car_SubBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_BrandId",
                table: "Car",
                newName: "IX_Car_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Brands_BrandId",
                table: "Car",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_SubBrands_SubBrandId",
                table: "Car",
                column: "SubBrandId",
                principalTable: "SubBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
