using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectElo.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UsarCriadorComoFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_AspNetUsers_CriadorEventoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_CriadorEventoId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "CriadorEventoId",
                table: "Eventos");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Criador",
                table: "Eventos",
                column: "Criador");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_AspNetUsers_Criador",
                table: "Eventos",
                column: "Criador",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_AspNetUsers_Criador",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_Criador",
                table: "Eventos");

            migrationBuilder.AddColumn<Guid>(
                name: "CriadorEventoId",
                table: "Eventos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CriadorEventoId",
                table: "Eventos",
                column: "CriadorEventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_AspNetUsers_CriadorEventoId",
                table: "Eventos",
                column: "CriadorEventoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
