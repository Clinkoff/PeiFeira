using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fase1_CriarDisciplinaPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_PerfilProfessor_PerfilProfessorOrientadorId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Semestre_SemestreId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Turma_TurmaId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Equipe_ProjetoId",
                table: "Equipe");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "Tema",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "Periodo",
                table: "PerfilAluno");

            migrationBuilder.RenameColumn(
                name: "PerfilProfessorOrientadorId",
                table: "Projeto",
                newName: "ProfessorOrientadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Projeto_PerfilProfessorOrientadorId",
                table: "Projeto",
                newName: "IX_Projeto_ProfessorOrientadorId");

            migrationBuilder.RenameColumn(
                name: "Semestre",
                table: "PerfilAluno",
                newName: "Turno");

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaPIId",
                table: "Projeto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DisciplinaPIId1",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_DisciplinaPIId1",
                table: "Projeto",
                column: "DisciplinaPIId1");

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_ProjetoId",
                table: "Equipe",
                column: "ProjetoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPI_IsActive",
                table: "DisciplinaPI",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPI_PerfilProfessorId",
                table: "DisciplinaPI",
                column: "PerfilProfessorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_DisciplinaPI_DisciplinaPIId1",
                table: "Projeto",
                column: "DisciplinaPIId1",
                principalTable: "DisciplinaPI",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_PerfilProfessor_ProfessorOrientadorId",
                table: "Projeto",
                column: "ProfessorOrientadorId",
                principalTable: "PerfilProfessor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Semestre_SemestreId",
                table: "Projeto",
                column: "SemestreId",
                principalTable: "Semestre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Turma_TurmaId",
                table: "Projeto",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_DisciplinaPI_DisciplinaPIId1",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_PerfilProfessor_ProfessorOrientadorId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Semestre_SemestreId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Turma_TurmaId",
                table: "Projeto");

            migrationBuilder.DropTable(
                name: "DisciplinaPITurma");

            migrationBuilder.DropTable(
                name: "DisciplinaPI");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_DisciplinaPIId1",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Equipe_ProjetoId",
                table: "Equipe");

            migrationBuilder.DropColumn(
                name: "DisciplinaPIId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "DisciplinaPIId1",
                table: "Projeto");

            migrationBuilder.RenameColumn(
                name: "ProfessorOrientadorId",
                table: "Projeto",
                newName: "PerfilProfessorOrientadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Projeto_ProfessorOrientadorId",
                table: "Projeto",
                newName: "IX_Projeto_PerfilProfessorOrientadorId");

            migrationBuilder.RenameColumn(
                name: "Turno",
                table: "PerfilAluno",
                newName: "Semestre");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Turma",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tema",
                table: "Projeto",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Periodo",
                table: "PerfilAluno",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_ProjetoId",
                table: "Equipe",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_PerfilProfessor_PerfilProfessorOrientadorId",
                table: "Projeto",
                column: "PerfilProfessorOrientadorId",
                principalTable: "PerfilProfessor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Semestre_SemestreId",
                table: "Projeto",
                column: "SemestreId",
                principalTable: "Semestre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Turma_TurmaId",
                table: "Projeto",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
