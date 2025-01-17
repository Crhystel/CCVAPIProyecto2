using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCVAPIProyecto2.Migrations
{
    /// <inheritdoc />
    public partial class Migracion4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Materia",
                table: "ClaseProfesores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Materia",
                table: "ClaseProfesores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
