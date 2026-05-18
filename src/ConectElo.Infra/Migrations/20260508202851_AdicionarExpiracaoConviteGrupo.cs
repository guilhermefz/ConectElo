using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectElo.Infra.Data
{
    /// <inheritdoc />
    public partial class AdicionarExpiracaoConviteGrupo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CodigoConviteExpiracao",
                table: "Grupos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoExpiracaoEConvite",
                table: "Grupos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_CodigoConvite",
                table: "Grupos",
                column: "CodigoConvite",
                unique: true,
                filter: "\"CodigoConvite\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grupos_CodigoConvite",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "CodigoConviteExpiracao",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "TipoExpiracaoEConvite",
                table: "Grupos");
        }
    }
}
