using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleAPI.Migrations
{
    public partial class CategoryIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CategoryId",
                table: "Vehicles",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Categories_CategoryId",
                table: "Vehicles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Categories_CategoryId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CategoryId",
                table: "Vehicles");
        }
    }
}
