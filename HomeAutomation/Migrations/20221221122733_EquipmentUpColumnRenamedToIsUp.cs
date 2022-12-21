using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeAutomation.Migrations
{
    public partial class EquipmentUpColumnRenamedToIsUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Up",
                table: "Equipments",
                newName: "IsUp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUp",
                table: "Equipments",
                newName: "Up");
        }
    }
}
