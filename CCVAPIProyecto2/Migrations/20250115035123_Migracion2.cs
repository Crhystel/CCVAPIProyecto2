using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCVAPIProyecto2.Migrations
{
    /// <inheritdoc />
    public partial class Migracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grado",
                table: "ClaseEstudiantes",
                newName: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaseEstudiantes_GradoId",
                table: "ClaseEstudiantes",
                column: "GradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaseEstudiantes_Grados_GradoId",
                table: "ClaseEstudiantes",
                column: "GradoId",
                principalTable: "Grados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaseEstudiantes_Grados_GradoId",
                table: "ClaseEstudiantes");

            migrationBuilder.DropIndex(
                name: "IX_ClaseEstudiantes_GradoId",
                table: "ClaseEstudiantes");

            migrationBuilder.RenameColumn(
                name: "GradoId",
                table: "ClaseEstudiantes",
                newName: "Grado");
        }
    }
}
