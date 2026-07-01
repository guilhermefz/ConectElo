using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConectElo.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarQuizAmigoSecreto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerguntasQuiz",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntasQuiz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpcoesQuiz",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PerguntaQuizId = table.Column<Guid>(type: "uuid", nullable: false),
                    Emoji = table.Column<string>(type: "text", nullable: true),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcoesQuiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcoesQuiz_PerguntasQuiz_PerguntaQuizId",
                        column: x => x.PerguntaQuizId,
                        principalTable: "PerguntasQuiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerguntasAmigoSecreto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultadoSorteioId = table.Column<Guid>(type: "uuid", nullable: false),
                    PerguntaQuizId = table.Column<Guid>(type: "uuid", nullable: false),
                    OpcaoRespostaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PerguntadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RespondidaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntasAmigoSecreto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerguntasAmigoSecreto_OpcoesQuiz_OpcaoRespostaId",
                        column: x => x.OpcaoRespostaId,
                        principalTable: "OpcoesQuiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerguntasAmigoSecreto_PerguntasQuiz_PerguntaQuizId",
                        column: x => x.PerguntaQuizId,
                        principalTable: "PerguntasQuiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerguntasAmigoSecreto_ResultadoSorteios_ResultadoSorteioId",
                        column: x => x.ResultadoSorteioId,
                        principalTable: "ResultadoSorteios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PerguntasQuiz",
                columns: new[] { "Id", "Ativa", "Texto" },
                values: new object[,]
                {
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000001"), true, "Qual presente te deixa mais feliz?" },
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000002"), true, "Você prefere ganhar…" },
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000003"), true, "Qual categoria mais combina com você?" },
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000004"), true, "Seu tipo de presente ideal é…" },
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000005"), true, "Qual sua estética?" },
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000006"), true, "Escolha uma cor" },
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000007"), true, "Você é mais…" },
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000008"), true, "Seu momento favorito do dia" },
                    { new Guid("b1f1c0d0-0000-0000-0000-000000000009"), true, "Final de semana perfeito" },
                    { new Guid("b1f1c0d0-0000-0000-0000-00000000000a"), true, "O que você menos gosta de ganhar?" },
                    { new Guid("b1f1c0d0-0000-0000-0000-00000000000b"), true, "Qual energia combina com você?" }
                });

            migrationBuilder.InsertData(
                table: "OpcoesQuiz",
                columns: new[] { "Id", "Emoji", "Ordem", "PerguntaQuizId", "Texto" },
                values: new object[,]
                {
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000001"), "📚", 1, new Guid("b1f1c0d0-0000-0000-0000-000000000001"), "Algo útil" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000002"), "😂", 2, new Guid("b1f1c0d0-0000-0000-0000-000000000001"), "Algo engraçado" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000003"), "✨", 3, new Guid("b1f1c0d0-0000-0000-0000-000000000001"), "Algo criativo" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000004"), "❤️", 4, new Guid("b1f1c0d0-0000-0000-0000-000000000001"), "Algo com significado" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000005"), "🎉", 5, new Guid("b1f1c0d0-0000-0000-0000-000000000001"), "Surpresa total" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000006"), "🍫", 1, new Guid("b1f1c0d0-0000-0000-0000-000000000002"), "Consumível" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000007"), "🧸", 2, new Guid("b1f1c0d0-0000-0000-0000-000000000002"), "Objeto" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000008"), "🎟️", 3, new Guid("b1f1c0d0-0000-0000-0000-000000000002"), "Experiência" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000009"), "💌", 4, new Guid("b1f1c0d0-0000-0000-0000-000000000002"), "Algo feito à mão" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000000a"), "📚", 1, new Guid("b1f1c0d0-0000-0000-0000-000000000003"), "Livros" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000000b"), "☕", 2, new Guid("b1f1c0d0-0000-0000-0000-000000000003"), "Café/Chá" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000000c"), "🎮", 3, new Guid("b1f1c0d0-0000-0000-0000-000000000003"), "Games" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000000d"), "🎧", 4, new Guid("b1f1c0d0-0000-0000-0000-000000000003"), "Música" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000000e"), "👕", 5, new Guid("b1f1c0d0-0000-0000-0000-000000000003"), "Roupas" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000000f"), "🏠", 6, new Guid("b1f1c0d0-0000-0000-0000-000000000003"), "Casa" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000010"), null, 1, new Guid("b1f1c0d0-0000-0000-0000-000000000004"), "Pequeno e útil" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000011"), null, 2, new Guid("b1f1c0d0-0000-0000-0000-000000000004"), "Diferente" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000012"), null, 3, new Guid("b1f1c0d0-0000-0000-0000-000000000004"), "Premium" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000013"), null, 4, new Guid("b1f1c0d0-0000-0000-0000-000000000004"), "Engraçado" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000014"), null, 5, new Guid("b1f1c0d0-0000-0000-0000-000000000004"), "Minimalista" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000015"), "🎨", 6, new Guid("b1f1c0d0-0000-0000-0000-000000000004"), "Estilo" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000016"), "⚫", 1, new Guid("b1f1c0d0-0000-0000-0000-000000000005"), "Minimalista" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000017"), "🌈", 2, new Guid("b1f1c0d0-0000-0000-0000-000000000005"), "Colorido" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000018"), "🪵", 3, new Guid("b1f1c0d0-0000-0000-0000-000000000005"), "Natural" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000019"), "✨", 4, new Guid("b1f1c0d0-0000-0000-0000-000000000005"), "Moderno" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000001a"), "🎮", 5, new Guid("b1f1c0d0-0000-0000-0000-000000000005"), "Geek" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000001b"), "⚫", 1, new Guid("b1f1c0d0-0000-0000-0000-000000000006"), "Preto" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000001c"), "⚪", 2, new Guid("b1f1c0d0-0000-0000-0000-000000000006"), "Branco" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000001d"), "🔵", 3, new Guid("b1f1c0d0-0000-0000-0000-000000000006"), "Azul" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000001e"), "🟢", 4, new Guid("b1f1c0d0-0000-0000-0000-000000000006"), "Verde" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000001f"), "🔴", 5, new Guid("b1f1c0d0-0000-0000-0000-000000000006"), "Vermelho" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000020"), "🟣", 6, new Guid("b1f1c0d0-0000-0000-0000-000000000006"), "Roxo" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000021"), "🏠", 1, new Guid("b1f1c0d0-0000-0000-0000-000000000007"), "Caseiro" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000022"), "✈️", 2, new Guid("b1f1c0d0-0000-0000-0000-000000000007"), "Explorador" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000023"), "☕", 3, new Guid("b1f1c0d0-0000-0000-0000-000000000007"), "Relax" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000024"), "🏃", 4, new Guid("b1f1c0d0-0000-0000-0000-000000000007"), "Ativo" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000025"), "🍕", 5, new Guid("b1f1c0d0-0000-0000-0000-000000000007"), "Hábitos" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000026"), "🌅", 1, new Guid("b1f1c0d0-0000-0000-0000-000000000008"), "Manhã" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000027"), "☀️", 2, new Guid("b1f1c0d0-0000-0000-0000-000000000008"), "Tarde" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000028"), "🌙", 3, new Guid("b1f1c0d0-0000-0000-0000-000000000008"), "Noite" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000029"), "🌃", 4, new Guid("b1f1c0d0-0000-0000-0000-000000000008"), "Madrugada" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000002a"), "🎬", 1, new Guid("b1f1c0d0-0000-0000-0000-000000000009"), "Filmes" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000002b"), "🎮", 2, new Guid("b1f1c0d0-0000-0000-0000-000000000009"), "Jogos" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000002c"), "🍽️", 3, new Guid("b1f1c0d0-0000-0000-0000-000000000009"), "Comer fora" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000002d"), "📚", 4, new Guid("b1f1c0d0-0000-0000-0000-000000000009"), "Ler" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000002e"), "💤", 5, new Guid("b1f1c0d0-0000-0000-0000-000000000009"), "Descansar" },
                    { new Guid("b2f2c0d0-0000-0000-0000-00000000002f"), null, 1, new Guid("b1f1c0d0-0000-0000-0000-00000000000a"), "Perfume" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000030"), null, 2, new Guid("b1f1c0d0-0000-0000-0000-00000000000a"), "Roupa" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000031"), null, 3, new Guid("b1f1c0d0-0000-0000-0000-00000000000a"), "Decoração" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000032"), null, 4, new Guid("b1f1c0d0-0000-0000-0000-00000000000a"), "Tecnologia" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000033"), null, 5, new Guid("b1f1c0d0-0000-0000-0000-00000000000a"), "Vale-presente" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000034"), null, 1, new Guid("b1f1c0d0-0000-0000-0000-00000000000b"), "Golden retriever" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000035"), null, 2, new Guid("b1f1c0d0-0000-0000-0000-00000000000b"), "Gato" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000036"), null, 3, new Guid("b1f1c0d0-0000-0000-0000-00000000000b"), "Coruja" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000037"), null, 4, new Guid("b1f1c0d0-0000-0000-0000-00000000000b"), "Capivara" },
                    { new Guid("b2f2c0d0-0000-0000-0000-000000000038"), null, 5, new Guid("b1f1c0d0-0000-0000-0000-00000000000b"), "Panda" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpcoesQuiz_PerguntaQuizId",
                table: "OpcoesQuiz",
                column: "PerguntaQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntasAmigoSecreto_OpcaoRespostaId",
                table: "PerguntasAmigoSecreto",
                column: "OpcaoRespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntasAmigoSecreto_PerguntaQuizId",
                table: "PerguntasAmigoSecreto",
                column: "PerguntaQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntasAmigoSecreto_ResultadoSorteioId",
                table: "PerguntasAmigoSecreto",
                column: "ResultadoSorteioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerguntasAmigoSecreto");

            migrationBuilder.DropTable(
                name: "OpcoesQuiz");

            migrationBuilder.DropTable(
                name: "PerguntasQuiz");
        }
    }
}
