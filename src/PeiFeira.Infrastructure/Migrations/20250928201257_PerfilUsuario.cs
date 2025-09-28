using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PerfilUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipe_Usuario_LiderId",
                table: "Equipe");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroEquipe_PefilAluno_PerfilAlunoId",
                table: "MembroEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroEquipe_Usuario_UsuarioId",
                table: "MembroEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_PefilAluno_Usuario_UsuarioId",
                table: "PefilAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Equipe_EquipeId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_PerfilProfessor_PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_EquipeId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_MembroEquipe_PerfilAlunoId",
                table: "MembroEquipe");

            migrationBuilder.DropIndex(
                name: "IX_MembroEquipe_Usuario_Equipe_Active",
                table: "MembroEquipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PefilAluno",
                table: "PefilAluno");

            migrationBuilder.DropColumn(
                name: "PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "MembroEquipe");

            migrationBuilder.RenameTable(
                name: "PefilAluno",
                newName: "PerfilAluno");

            migrationBuilder.RenameColumn(
                name: "EquipeId",
                table: "Projeto",
                newName: "TurmaId");

            migrationBuilder.RenameColumn(
                name: "LiderId",
                table: "Equipe",
                newName: "ProjetoId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipe_LiderId",
                table: "Equipe",
                newName: "IX_Equipe_ProjetoId");

            migrationBuilder.RenameIndex(
                name: "IX_PefilAluno_UsuarioId",
                table: "PerfilAluno",
                newName: "IX_PerfilAluno_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_PefilAluno_IsActive",
                table: "PerfilAluno",
                newName: "IX_PerfilAluno_IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "Tema",
                table: "Projeto",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "RedesSociaisResponsavel",
                table: "Projeto",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedeSocial",
                table: "Projeto",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeEmpresa",
                table: "Projeto",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailResponsavel",
                table: "Projeto",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contato",
                table: "Projeto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CargoResponsavel",
                table: "Projeto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PerfilProfessorOrientadorId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SemestreId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Projeto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "PerfilAlunoId",
                table: "MembroEquipe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Funcao",
                table: "MembroEquipe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Cargo",
                table: "MembroEquipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "LiderPerfilAlunoId",
                table: "Equipe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "PontuacaoTotal",
                table: "Avaliacao",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaFinal",
                table: "Avaliacao",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjetoId",
                table: "Avaliacao",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Periodo",
                table: "PerfilAluno",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Semestre",
                table: "PerfilAluno",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerfilAluno",
                table: "PerfilAluno",
                column: "Id");

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
                    Tipo = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_PerfilProfessorOrientadorId",
                table: "Projeto",
                column: "PerfilProfessorOrientadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_SemestreId",
                table: "Projeto",
                column: "SemestreId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_Status",
                table: "Projeto",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_TurmaId",
                table: "Projeto",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_PerfilAluno_Equipe_Active",
                table: "MembroEquipe",
                columns: new[] { "PerfilAlunoId", "EquipeId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_LiderPerfilAlunoId",
                table: "Equipe",
                column: "LiderPerfilAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_ProjetoId",
                table: "Avaliacao",
                column: "ProjetoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_Projeto_ProjetoId",
                table: "Avaliacao",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipe_PerfilAluno_LiderPerfilAlunoId",
                table: "Equipe",
                column: "LiderPerfilAlunoId",
                principalTable: "PerfilAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipe_Projeto_ProjetoId",
                table: "Equipe",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembroEquipe_PerfilAluno_PerfilAlunoId",
                table: "MembroEquipe",
                column: "PerfilAlunoId",
                principalTable: "PerfilAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilAluno_Usuario_UsuarioId",
                table: "PerfilAluno",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Projeto_ProjetoId",
                table: "Avaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipe_PerfilAluno_LiderPerfilAlunoId",
                table: "Equipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipe_Projeto_ProjetoId",
                table: "Equipe");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroEquipe_PerfilAluno_PerfilAlunoId",
                table: "MembroEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_PerfilAluno_Usuario_UsuarioId",
                table: "PerfilAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_PerfilProfessor_PerfilProfessorOrientadorId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Semestre_SemestreId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Turma_TurmaId",
                table: "Projeto");

            migrationBuilder.DropTable(
                name: "AlunosTurma");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "Semestre");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_PerfilProfessorOrientadorId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_SemestreId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_Status",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_TurmaId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_MembroEquipe_PerfilAluno_Equipe_Active",
                table: "MembroEquipe");

            migrationBuilder.DropIndex(
                name: "IX_Equipe_LiderPerfilAlunoId",
                table: "Equipe");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacao_ProjetoId",
                table: "Avaliacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerfilAluno",
                table: "PerfilAluno");

            migrationBuilder.DropColumn(
                name: "PerfilProfessorOrientadorId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "SemestreId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "MembroEquipe");

            migrationBuilder.DropColumn(
                name: "LiderPerfilAlunoId",
                table: "Equipe");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "Semestre",
                table: "PerfilAluno");

            migrationBuilder.RenameTable(
                name: "PerfilAluno",
                newName: "PefilAluno");

            migrationBuilder.RenameColumn(
                name: "TurmaId",
                table: "Projeto",
                newName: "EquipeId");

            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "Equipe",
                newName: "LiderId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipe_ProjetoId",
                table: "Equipe",
                newName: "IX_Equipe_LiderId");

            migrationBuilder.RenameIndex(
                name: "IX_PerfilAluno_UsuarioId",
                table: "PefilAluno",
                newName: "IX_PefilAluno_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_PerfilAluno_IsActive",
                table: "PefilAluno",
                newName: "IX_PefilAluno_IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "Tema",
                table: "Projeto",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "RedesSociaisResponsavel",
                table: "Projeto",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedeSocial",
                table: "Projeto",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeEmpresa",
                table: "Projeto",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailResponsavel",
                table: "Projeto",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contato",
                table: "Projeto",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CargoResponsavel",
                table: "Projeto",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PerfilProfessorId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PerfilAlunoId",
                table: "MembroEquipe",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Funcao",
                table: "MembroEquipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "MembroEquipe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "PontuacaoTotal",
                table: "Avaliacao",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaFinal",
                table: "Avaliacao",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Periodo",
                table: "PefilAluno",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PefilAluno",
                table: "PefilAluno",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_EquipeId",
                table: "Projeto",
                column: "EquipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_PerfilProfessorId",
                table: "Projeto",
                column: "PerfilProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_PerfilAlunoId",
                table: "MembroEquipe",
                column: "PerfilAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_Usuario_Equipe_Active",
                table: "MembroEquipe",
                columns: new[] { "UsuarioId", "EquipeId", "IsActive" });

            migrationBuilder.AddForeignKey(
                name: "FK_Equipe_Usuario_LiderId",
                table: "Equipe",
                column: "LiderId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MembroEquipe_PefilAluno_PerfilAlunoId",
                table: "MembroEquipe",
                column: "PerfilAlunoId",
                principalTable: "PefilAluno",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MembroEquipe_Usuario_UsuarioId",
                table: "MembroEquipe",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PefilAluno_Usuario_UsuarioId",
                table: "PefilAluno",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Equipe_EquipeId",
                table: "Projeto",
                column: "EquipeId",
                principalTable: "Equipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_PerfilProfessor_PerfilProfessorId",
                table: "Projeto",
                column: "PerfilProfessorId",
                principalTable: "PerfilProfessor",
                principalColumn: "Id");
        }
    }
}
