using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeAutomation.Migrations
{
    public partial class EquipmentCompositeUniqueIndexNameAndRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Equipments_Name",
                table: "Equipments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Name",
                table: "Equipments",
                column: "Name",
                unique: true);
        }
    }
}
