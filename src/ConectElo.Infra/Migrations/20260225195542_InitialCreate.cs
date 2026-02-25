using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectElo.Infra.Data
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmigoSecretoEventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false),
                    DataSorteio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResultadoSorteio = table.Column<string>(type: "text", nullable: false),
                    Sorteado = table.Column<bool>(type: "boolean", nullable: false),
                    EventoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmigoSecretoEventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GaleriaFotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UrlFoto = table.Column<string>(type: "text", nullable: false),
                    DataUpload = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GaleriaFotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    CodigoConvite = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataDelecao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ImgGrupo = table.Column<string>(type: "text", nullable: true),
                    Privado = table.Column<bool>(type: "boolean", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProprietarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    MuralId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListaDesejos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDesejos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mural", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    LinkUrl = table.Column<string>(type: "text", nullable: false),
                    NotificacaoLida = table.Column<bool>(type: "boolean", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoNotificacao = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CPF = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    FotoPerdilUrl = table.Column<string>(type: "text", nullable: true),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    DataCriacaoConta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Genero = table.Column<int>(type: "integer", nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAtivo = table.Column<bool>(type: "boolean", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    DataDelecao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BanimentoGrupo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BanidoPor = table.Column<Guid>(type: "uuid", nullable: false),
                    DataBanimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanimentoGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanimentoGrupo_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BanimentoGrupo_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataDelecao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Localizacao = table.Column<string>(type: "text", nullable: true),
                    Criador = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadorEventoId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TipoEvento = table.Column<int>(type: "integer", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Usuarios_CriadorEventoId",
                        column: x => x.CriadorEventoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItensListaDesejos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    UrlReference = table.Column<string>(type: "text", nullable: false),
                    ListaDesejosId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservadoPorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensListaDesejos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensListaDesejos_ListaDesejos_ListaDesejosId",
                        column: x => x.ListaDesejosId,
                        principalTable: "ListaDesejos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensListaDesejos_Usuarios_ReservadoPorId",
                        column: x => x.ReservadoPorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "membrosGrupos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membrosGrupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_membrosGrupos_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_membrosGrupos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    HorarioEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Anonima = table.Column<bool>(type: "boolean", nullable: false),
                    TipoMidia = table.Column<int>(type: "integer", nullable: false),
                    MidiaUrl = table.Column<string>(type: "text", nullable: true),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    DataPostagem = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    MuralId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Postagens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoEntrada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoEntrada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoEntrada_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoEntrada_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmacaoEventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmacaoEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmacaoEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfirmacaoEventos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConviteEventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenConvite = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConviteEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConviteEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConviteEventos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BanimentoGrupo_GrupoId",
                table: "BanimentoGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_BanimentoGrupo_UsuarioId",
                table: "BanimentoGrupo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmacaoEventos_EventoId",
                table: "ConfirmacaoEventos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmacaoEventos_UsuarioId",
                table: "ConfirmacaoEventos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ConviteEventos_EventoId",
                table: "ConviteEventos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConviteEventos_UsuarioId",
                table: "ConviteEventos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CriadorEventoId",
                table: "Eventos",
                column: "CriadorEventoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensListaDesejos_ListaDesejosId",
                table: "ItensListaDesejos",
                column: "ListaDesejosId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensListaDesejos_ReservadoPorId",
                table: "ItensListaDesejos",
                column: "ReservadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_membrosGrupos_GrupoId",
                table: "membrosGrupos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_membrosGrupos_UsuarioId",
                table: "membrosGrupos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_UsuarioId",
                table: "Mensagens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_UsuarioId",
                table: "Postagens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoEntrada_GrupoId",
                table: "SolicitacaoEntrada",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoEntrada_UsuarioId",
                table: "SolicitacaoEntrada",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmigoSecretoEventos");

            migrationBuilder.DropTable(
                name: "BanimentoGrupo");

            migrationBuilder.DropTable(
                name: "ConfirmacaoEventos");

            migrationBuilder.DropTable(
                name: "ConviteEventos");

            migrationBuilder.DropTable(
                name: "GaleriaFotos");

            migrationBuilder.DropTable(
                name: "ItensListaDesejos");

            migrationBuilder.DropTable(
                name: "membrosGrupos");

            migrationBuilder.DropTable(
                name: "Mensagens");

            migrationBuilder.DropTable(
                name: "Mural");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "Postagens");

            migrationBuilder.DropTable(
                name: "SolicitacaoEntrada");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "ListaDesejos");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
