using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectElo.Infra.Data
{
    /// <inheritdoc />
    public partial class AddFeedIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_membrosGrupos_UsuarioId",
                table: "membrosGrupos",
                newName: "IX_MembrosGrupo_UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_MuralId_DataPostagem",
                table: "Postagens",
                columns: new[] { "MuralId", "DataPostagem" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Postagens_MuralId_DataPostagem",
                table: "Postagens");

            migrationBuilder.RenameIndex(
                name: "IX_MembrosGrupo_UsuarioId",
                table: "membrosGrupos",
                newName: "IX_membrosGrupos_UsuarioId");
        }
    }
}
