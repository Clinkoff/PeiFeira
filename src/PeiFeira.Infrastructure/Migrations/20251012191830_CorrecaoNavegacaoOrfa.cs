using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoNavegacaoOrfa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Projeto_ProjetoId",
                table: "Avaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_PerfilProfessor_PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Semestre_SemestreId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Turma_TurmaId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_SemestreId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_TurmaId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacao_ProjetoId",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "SemestreId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Avaliacao");

            migrationBuilder.AddColumn<Guid>(
                name: "PerfilProfessorId1",
                table: "DisciplinaPI",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPI_PerfilProfessorId1",
                table: "DisciplinaPI",
                column: "PerfilProfessorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaPI_PerfilProfessor_PerfilProfessorId1",
                table: "DisciplinaPI",
                column: "PerfilProfessorId1",
                principalTable: "PerfilProfessor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaPI_PerfilProfessor_PerfilProfessorId1",
                table: "DisciplinaPI");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaPI_PerfilProfessorId1",
                table: "DisciplinaPI");

            migrationBuilder.DropColumn(
                name: "PerfilProfessorId1",
                table: "DisciplinaPI");

            migrationBuilder.AddColumn<Guid>(
                name: "PerfilProfessorId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SemestreId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TurmaId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjetoId",
                table: "Avaliacao",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_PerfilProfessorId",
                table: "Projeto",
                column: "PerfilProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_SemestreId",
                table: "Projeto",
                column: "SemestreId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_TurmaId",
                table: "Projeto",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_ProjetoId",
                table: "Avaliacao",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_Projeto_ProjetoId",
                table: "Avaliacao",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_PerfilProfessor_PerfilProfessorId",
                table: "Projeto",
                column: "PerfilProfessorId",
                principalTable: "PerfilProfessor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Semestre_SemestreId",
                table: "Projeto",
                column: "SemestreId",
                principalTable: "Semestre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Turma_TurmaId",
                table: "Projeto",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id");
        }
    }
}
