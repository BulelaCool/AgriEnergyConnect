using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergyConnect.Migrations
{
    /// <inheritdoc />
    public partial class FixProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Farmers_FarmerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "FarmerId",
                table: "Products",
                newName: "FarmerID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_FarmerId",
                table: "Products",
                newName: "IX_Products_FarmerID");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Farmers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FarmerID",
                table: "Farmers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Farmers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Farmers_FarmerID",
                table: "Products",
                column: "FarmerID",
                principalTable: "Farmers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Farmers_FarmerID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "FarmerID",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Farmers");

            migrationBuilder.RenameColumn(
                name: "FarmerID",
                table: "Products",
                newName: "FarmerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_FarmerID",
                table: "Products",
                newName: "IX_Products_FarmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Farmers_FarmerId",
                table: "Products",
                column: "FarmerId",
                principalTable: "Farmers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
