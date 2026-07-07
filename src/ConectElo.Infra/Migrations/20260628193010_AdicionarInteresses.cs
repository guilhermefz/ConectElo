using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConectElo.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarInteresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InteresseUsuario",
                columns: table => new
                {
                    InteressesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuariosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteresseUsuario", x => new { x.InteressesId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_InteresseUsuario_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteresseUsuario_Interesses_InteressesId",
                        column: x => x.InteressesId,
                        principalTable: "Interesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interesses",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000001"), "Café" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000002"), "Sushi" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000003"), "Cozinhar" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000004"), "Vinho" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000005"), "Música" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000006"), "Filmes" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000007"), "Livros" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000008"), "Games" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000009"), "Academia" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000000a"), "Corrida" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000000b"), "Futebol" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000000c"), "Boxe" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000000d"), "Praia" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000000e"), "Viagens" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000000f"), "Fotografia" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000010"), "Arte" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000011"), "Museus" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000012"), "Tecnologia" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000013"), "Programação" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000014"), "Carros" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000015"), "Moda" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000016"), "Tatuagens" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000017"), "Cachorros" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000018"), "Jardinagem" },
                    { new Guid("a1f1c0d0-0000-0000-0000-000000000019"), "Jogos de tabuleiro" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000001a"), "Podcasts" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000001b"), "Astrologia" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000001c"), "Festivais" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000001d"), "Baladas" },
                    { new Guid("a1f1c0d0-0000-0000-0000-00000000001e"), "Stand-up Comedy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InteresseUsuario_UsuariosId",
                table: "InteresseUsuario",
                column: "UsuariosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InteresseUsuario");

            migrationBuilder.DropTable(
                name: "Interesses");
        }
    }
}
