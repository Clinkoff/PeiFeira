using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fase3_RefatorarProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Projeto_ProfessorOrientadorId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "ProfessorOrientadorId",
                table: "Projeto");

            migrationBuilder.RenameColumn(
                name: "DisciplinaPIId1",
                table: "Projeto",
                newName: "PerfilProfessorId");

            migrationBuilder.RenameIndex(
                name: "IX_Projeto_DisciplinaPIId1",
                table: "Projeto",
                newName: "IX_Projeto_PerfilProfessorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TurmaId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SemestreId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DisciplinaPIId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_DisciplinaPIId",
                table: "Projeto",
                column: "DisciplinaPIId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_DisciplinaPI_DisciplinaPIId",
                table: "Projeto",
                column: "DisciplinaPIId",
                principalTable: "DisciplinaPI",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_DisciplinaPI_DisciplinaPIId",
                table: "Projeto");

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
                name: "IX_Projeto_DisciplinaPIId",
                table: "Projeto");

            migrationBuilder.RenameColumn(
                name: "PerfilProfessorId",
                table: "Projeto",
                newName: "DisciplinaPIId1");

            migrationBuilder.RenameIndex(
                name: "IX_Projeto_PerfilProfessorId",
                table: "Projeto",
                newName: "IX_Projeto_DisciplinaPIId1");

            migrationBuilder.AlterColumn<Guid>(
                name: "TurmaId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SemestreId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DisciplinaPIId",
                table: "Projeto",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessorOrientadorId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ProfessorOrientadorId",
                table: "Projeto",
                column: "ProfessorOrientadorId");

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
    }
}
