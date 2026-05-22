using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectElo.Infra.Data
{
    /// <inheritdoc />
    public partial class RegistrarAniversarioEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Eventos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ListaDesejosId",
                table: "Eventos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeAniversariante",
                table: "Eventos",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ListaDesejosId",
                table: "Eventos",
                column: "ListaDesejosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_ListaDesejos_ListaDesejosId",
                table: "Eventos",
                column: "ListaDesejosId",
                principalTable: "ListaDesejos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_ListaDesejos_ListaDesejosId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_ListaDesejosId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "ListaDesejosId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "NomeAniversariante",
                table: "Eventos");
        }
    }
}
