using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectElo.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AmigoSecretoSorteio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultadoSorteio",
                table: "Eventos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExecucaoSorteio",
                table: "Eventos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusSorteio",
                table: "Eventos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResultadoSorteios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PresenteadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecebedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataSorteio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoSorteios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadoSorteios_AspNetUsers_PresenteadorId",
                        column: x => x.PresenteadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadoSorteios_AspNetUsers_RecebedorId",
                        column: x => x.RecebedorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadoSorteios_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MensagensAnonimas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultadoSorteioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    HorarioEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParticipanteTipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagensAnonimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensagensAnonimas_ResultadoSorteios_ResultadoSorteioId",
                        column: x => x.ResultadoSorteioId,
                        principalTable: "ResultadoSorteios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MensagensAnonimas_ResultadoSorteioId",
                table: "MensagensAnonimas",
                column: "ResultadoSorteioId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoSorteios_EventoId",
                table: "ResultadoSorteios",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoSorteios_PresenteadorId",
                table: "ResultadoSorteios",
                column: "PresenteadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoSorteios_RecebedorId",
                table: "ResultadoSorteios",
                column: "RecebedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MensagensAnonimas");

            migrationBuilder.DropTable(
                name: "ResultadoSorteios");

            migrationBuilder.DropColumn(
                name: "DataExecucaoSorteio",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "StatusSorteio",
                table: "Eventos");

            migrationBuilder.AddColumn<string>(
                name: "ResultadoSorteio",
                table: "Eventos",
                type: "text",
                nullable: true);
        }
    }
}
