using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEquipeProjetoId_AddProjetoUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Usuario_AvaliadorId",
                table: "Avaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaPI_PerfilProfessor_PerfilProfessorId1",
                table: "DisciplinaPI");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipe_Projeto_ProjetoId",
                table: "Equipe");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_Status",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Equipe_ProjetoId",
                table: "Equipe");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaPI_PerfilProfessorId1",
                table: "DisciplinaPI");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Equipe");

            migrationBuilder.DropColumn(
                name: "PerfilProfessorId1",
                table: "DisciplinaPI");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Projeto",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "TelefoneResponsavel",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedesSociaisResponsavel",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedeSocial",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnderecoCompleto",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contato",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EquipeId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Avaliacao",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_Disciplina_Equipe_Unique",
                table: "Projeto",
                columns: new[] { "DisciplinaPIId", "EquipeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_EquipeId",
                table: "Projeto",
                column: "EquipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_UsuarioId",
                table: "Avaliacao",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_PerfilProfessor_AvaliadorId",
                table: "Avaliacao",
                column: "AvaliadorId",
                principalTable: "PerfilProfessor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_Usuario_UsuarioId",
                table: "Avaliacao",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_Equipe_EquipeId",
                table: "Projeto",
                column: "EquipeId",
                principalTable: "Equipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_PerfilProfessor_AvaliadorId",
                table: "Avaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Usuario_UsuarioId",
                table: "Avaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Equipe_EquipeId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_Disciplina_Equipe_Unique",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_EquipeId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacao_UsuarioId",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "EquipeId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Avaliacao");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Projeto",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "TelefoneResponsavel",
                table: "Projeto",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedesSociaisResponsavel",
                table: "Projeto",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedeSocial",
                table: "Projeto",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnderecoCompleto",
                table: "Projeto",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contato",
                table: "Projeto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjetoId",
                table: "Equipe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PerfilProfessorId1",
                table: "DisciplinaPI",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_Status",
                table: "Projeto",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_ProjetoId",
                table: "Equipe",
                column: "ProjetoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaPI_PerfilProfessorId1",
                table: "DisciplinaPI",
                column: "PerfilProfessorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_Usuario_AvaliadorId",
                table: "Avaliacao",
                column: "AvaliadorId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaPI_PerfilProfessor_PerfilProfessorId1",
                table: "DisciplinaPI",
                column: "PerfilProfessorId1",
                principalTable: "PerfilProfessor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipe_Projeto_ProjetoId",
                table: "Equipe",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
