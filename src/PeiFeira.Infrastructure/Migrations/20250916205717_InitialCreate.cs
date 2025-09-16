using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LiderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlQrCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CodigoConvite = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipes_Usuarios_LiderId",
                        column: x => x.LiderId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvaliadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelevanciaProblema = table.Column<int>(type: "int", nullable: false),
                    FundamentacaoProblema = table.Column<int>(type: "int", nullable: false),
                    FocoSolucao = table.Column<int>(type: "int", nullable: false),
                    ViabilidadeSolucao = table.Column<int>(type: "int", nullable: false),
                    ClarezaApresentacao = table.Column<int>(type: "int", nullable: false),
                    DominioAssunto = table.Column<int>(type: "int", nullable: false),
                    TransmissaoInformacoes = table.Column<int>(type: "int", nullable: false),
                    PadronizacaoApresentacao = table.Column<int>(type: "int", nullable: false),
                    LinguagemTempo = table.Column<int>(type: "int", nullable: false),
                    QualidadeRespostas = table.Column<int>(type: "int", nullable: false),
                    PontuacaoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NotaFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Usuarios_AvaliadorId",
                        column: x => x.AvaliadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembrosEquipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Funcao = table.Column<int>(type: "int", nullable: false),
                    IngressouEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaiuEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembrosEquipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembrosEquipe_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembrosEquipe_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tema = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DesafioProposto = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EnderecoCompleto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RedeSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Contato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CargoResponsavel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelefoneResponsavel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailResponsavel = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RedesSociaisResponsavel = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projetos_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_AvaliadorId",
                table: "Avaliacoes",
                column: "AvaliadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_EquipeId",
                table: "Avaliacoes",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_CodigoConvite",
                table: "Equipes",
                column: "CodigoConvite",
                unique: true,
                filter: "[CodigoConvite] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_LiderId",
                table: "Equipes",
                column: "LiderId");

            migrationBuilder.CreateIndex(
                name: "IX_MembrosEquipe_EquipeId",
                table: "MembrosEquipe",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_MembrosEquipe_UsuarioId_EquipeId",
                table: "MembrosEquipe",
                columns: new[] { "UsuarioId", "EquipeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_EquipeId",
                table: "Projetos",
                column: "EquipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Matricula",
                table: "Usuarios",
                column: "Matricula",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "MembrosEquipe");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
