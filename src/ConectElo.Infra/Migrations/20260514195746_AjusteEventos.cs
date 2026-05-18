using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectElo.Infra.Data
{
    /// <inheritdoc />
    public partial class AjusteEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmigoSecretoEventos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataSorteio",
                table: "Eventos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Eventos",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResultadoSorteio",
                table: "Eventos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sorteado",
                table: "Eventos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Valor",
                table: "Eventos",
                type: "double precision",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_GrupoId",
                table: "Eventos",
                column: "GrupoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Grupos_GrupoId",
                table: "Eventos",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Grupos_GrupoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_GrupoId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "DataSorteio",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "ResultadoSorteio",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Sorteado",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Eventos");

            migrationBuilder.CreateTable(
                name: "AmigoSecretoEventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataSorteio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultadoSorteio = table.Column<string>(type: "text", nullable: false),
                    Sorteado = table.Column<bool>(type: "boolean", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmigoSecretoEventos", x => x.Id);
                });
        }
    }
}
