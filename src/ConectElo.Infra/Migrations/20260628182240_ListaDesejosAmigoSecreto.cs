using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectElo.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ListaDesejosAmigoSecreto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventoId",
                table: "ListaDesejos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "ListaDesejos",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventoId",
                table: "ListaDesejos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ListaDesejos");
        }
    }
}
