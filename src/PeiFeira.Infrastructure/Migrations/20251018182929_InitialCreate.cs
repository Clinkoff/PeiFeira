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
                name: "Semestre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semestre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SemestreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Curso = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Periodo = table.Column<int>(type: "int", nullable: true),
                    Turno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turma_Semestre_SemestreId",
                        column: x => x.SemestreId,
                        principalTable: "Semestre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerfilAluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Curso = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Turno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilAluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilAluno_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilProfessor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AreaEspecializacao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Titulacao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilProfessor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilProfessor_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunosTurma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerfilAlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataMatricula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAtual = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosTurma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlunosTurma_PerfilAluno_PerfilAlunoId",
                        column: x => x.PerfilAlunoId,
                        principalTable: "PerfilAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosTurma_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaPI",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SemestreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerfilProfessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TemaGeral = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Objetivos = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PerfilProfessorId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaPI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinaPI_PerfilProfessor_PerfilProfessorId",
                        column: x => x.PerfilProfessorId,
                        principalTable: "PerfilProfessor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisciplinaPI_PerfilProfessor_PerfilProfessorId1",
                        column: x => x.PerfilProfessorId1,
                        principalTable: "PerfilProfessor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DisciplinaPI_Semestre_SemestreId",
                        column: x => x.SemestreId,
                        principalTable: "Semestre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaPITurma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisciplinaPIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaPITurma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinaPITurma_DisciplinaPI_DisciplinaPIId",
                        column: x => x.DisciplinaPIId,
                        principalTable: "DisciplinaPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinaPITurma_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisciplinaPIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DesafioProposto = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EnderecoCompleto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RedeSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Contato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CargoResponsavel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelefoneResponsavel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailResponsavel = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RedesSociaisResponsavel = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projeto_DisciplinaPI_DisciplinaPIId",
                        column: x => x.DisciplinaPIId,
                        principalTable: "DisciplinaPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProjetoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LiderPerfilAlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UrlQrCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CodigoConvite = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipe_PerfilAluno_LiderPerfilAlunoId",
                        column: x => x.LiderPerfilAlunoId,
                        principalTable: "PerfilAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equipe_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
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
                    PontuacaoTotal = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    NotaFinal = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Usuario_AvaliadorId",
                        column: x => x.AvaliadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConviteEquipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConvidadoPorPerfilAlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConvidadoPerfilAlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MotivoResposta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RespondidoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConviteEquipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConviteEquipe_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConviteEquipe_PerfilAluno_ConvidadoPerfilAlunoId",
                        column: x => x.ConvidadoPerfilAlunoId,
                        principalTable: "PerfilAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConviteEquipe_PerfilAluno_ConvidadoPorPerfilAlunoId",
                        column: x => x.ConvidadoPorPerfilAlunoId,
                        principalTable: "PerfilAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembroEquipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerfilAlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false),
                    Funcao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IngressouEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaiuEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembroEquipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembroEquipe_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembroEquipe_PerfilAluno_PerfilAlunoId",
                        column: x => x.PerfilAlunoId,
                        principalTable: "PerfilAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunosTurma_IsActive",
                table: "AlunosTurma",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AlunosTurma_PerfilAlunoId_TurmaId_IsAtual",
                table: "AlunosTurma",
                columns: new[] { "PerfilAlunoId", "TurmaId", "IsAtual" });

            migrationBuilder.CreateIndex(
                name: "IX_AlunosTurma_TurmaId",
                table: "AlunosTurma",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_AvaliadorId",
                table: "Avaliacao",
                column: "AvaliadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_Equipe_Avaliador_Unique",
                table: "Avaliacao",
                columns: new[] { "EquipeId", "AvaliadorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_IsActive",
                table: "Avaliacao",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_NotaFinal",
                table: "Avaliacao",
                column: "NotaFinal");

            migrationBuilder.CreateIndex(
                name: "IX_ConviteEquipe_ConvidadoPerfilAlunoId",
                table: "ConviteEquipe",
                column: "ConvidadoPerfilAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConviteEquipe_ConvidadoPorPerfilAlunoId",
                table: "ConviteEquipe",
                column: "ConvidadoPorPerfilAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConviteEquipe_Equipe_Convidado_Unique",
                table: "ConviteEquipe",
                columns: new[] { "EquipeId", "ConvidadoPerfilAlunoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConviteEquipe_Status",
                table: "ConviteEquipe",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPI_IsActive",
                table: "DisciplinaPI",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPI_PerfilProfessorId",
                table: "DisciplinaPI",
                column: "PerfilProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPI_PerfilProfessorId1",
                table: "DisciplinaPI",
                column: "PerfilProfessorId1");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPI_SemestreId",
                table: "DisciplinaPI",
                column: "SemestreId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPITurma_Disciplina_Turma_Unique",
                table: "DisciplinaPITurma",
                columns: new[] { "DisciplinaPIId", "TurmaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPITurma_IsActive",
                table: "DisciplinaPITurma",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPITurma_TurmaId",
                table: "DisciplinaPITurma",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_CodigoConvite",
                table: "Equipe",
                column: "CodigoConvite",
                unique: true,
                filter: "[CodigoConvite] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_IsActive",
                table: "Equipe",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_LiderPerfilAlunoId",
                table: "Equipe",
                column: "LiderPerfilAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_ProjetoId",
                table: "Equipe",
                column: "ProjetoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_EquipeId",
                table: "MembroEquipe",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_IsActive",
                table: "MembroEquipe",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_PerfilAluno_Equipe_Active",
                table: "MembroEquipe",
                columns: new[] { "PerfilAlunoId", "EquipeId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_PerfilAluno_IsActive",
                table: "PerfilAluno",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilAluno_UsuarioId",
                table: "PerfilAluno",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilProfessor_IsActive",
                table: "PerfilProfessor",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilProfessor_UsuarioId",
                table: "PerfilProfessor",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_DisciplinaPIId",
                table: "Projeto",
                column: "DisciplinaPIId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_IsActive",
                table: "Projeto",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_Status",
                table: "Projeto",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Semestre_Ano_Periodo",
                table: "Semestre",
                columns: new[] { "Ano", "Periodo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semestre_IsActive",
                table: "Semestre",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_Codigo",
                table: "Turma",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turma_IsActive",
                table: "Turma",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_SemestreId",
                table: "Turma",
                column: "SemestreId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IsActive",
                table: "Usuario",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Matricula",
                table: "Usuario",
                column: "Matricula",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunosTurma");

            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "ConviteEquipe");

            migrationBuilder.DropTable(
                name: "DisciplinaPITurma");

            migrationBuilder.DropTable(
                name: "MembroEquipe");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "Equipe");

            migrationBuilder.DropTable(
                name: "PerfilAluno");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "DisciplinaPI");

            migrationBuilder.DropTable(
                name: "PerfilProfessor");

            migrationBuilder.DropTable(
                name: "Semestre");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
