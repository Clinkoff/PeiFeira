using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MembrosEquipe_UsuarioId_EquipeId",
                table: "MembrosEquipe");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_EquipeId",
                table: "Avaliacoes");

            migrationBuilder.AlterColumn<string>(
                name: "SenhaHash",
                table: "Usuarios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Projetos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Tema",
                table: "Projetos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RedesSociaisResponsavel",
                table: "Projetos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedeSocial",
                table: "Projetos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeResponsavel",
                table: "Projetos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeEmpresa",
                table: "Projetos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailResponsavel",
                table: "Projetos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DesafioProposto",
                table: "Projetos",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Contato",
                table: "Projetos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CargoResponsavel",
                table: "Projetos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Equipes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "PontuacaoTotal",
                table: "Avaliacoes",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaFinal",
                table: "Avaliacoes",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comentarios",
                table: "Avaliacoes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "AlteradoEm", "CriadoEm", "DeletadoEm", "Email", "IsActive", "Matricula", "Nome", "Role", "SenhaHash" },
                values: new object[] { new Guid("9088e821-3661-43be-a874-57d24304beda"), null, new DateTime(2025, 9, 16, 21, 5, 6, 313, DateTimeKind.Utc).AddTicks(1408), null, "admin@univag.edu.br", true, "0000000001", "Administrador", 2, "admin123" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IsActive",
                table: "Usuarios",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_IsActive",
                table: "Projetos",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_Usuario_Equipe_Active",
                table: "MembrosEquipe",
                columns: new[] { "UsuarioId", "EquipeId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_MembrosEquipe_IsActive",
                table: "MembrosEquipe",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_IsActive",
                table: "Equipes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_Equipe_Avaliador_Unique",
                table: "Avaliacoes",
                columns: new[] { "EquipeId", "AvaliadorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_IsActive",
                table: "Avaliacoes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_NotaFinal",
                table: "Avaliacoes",
                column: "NotaFinal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IsActive",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_IsActive",
                table: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_MembroEquipe_Usuario_Equipe_Active",
                table: "MembrosEquipe");

            migrationBuilder.DropIndex(
                name: "IX_MembrosEquipe_IsActive",
                table: "MembrosEquipe");

            migrationBuilder.DropIndex(
                name: "IX_Equipes_IsActive",
                table: "Equipes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacao_Equipe_Avaliador_Unique",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_IsActive",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_NotaFinal",
                table: "Avaliacoes");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("9088e821-3661-43be-a874-57d24304beda"));

            migrationBuilder.AlterColumn<string>(
                name: "SenhaHash",
                table: "Usuarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Projetos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Tema",
                table: "Projetos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "RedesSociaisResponsavel",
                table: "Projetos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RedeSocial",
                table: "Projetos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeResponsavel",
                table: "Projetos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeEmpresa",
                table: "Projetos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailResponsavel",
                table: "Projetos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DesafioProposto",
                table: "Projetos",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Contato",
                table: "Projetos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CargoResponsavel",
                table: "Projetos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Equipes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<decimal>(
                name: "PontuacaoTotal",
                table: "Avaliacoes",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaFinal",
                table: "Avaliacoes",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Comentarios",
                table: "Avaliacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembrosEquipe_UsuarioId_EquipeId",
                table: "MembrosEquipe",
                columns: new[] { "UsuarioId", "EquipeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_EquipeId",
                table: "Avaliacoes",
                column: "EquipeId");
        }
    }
}
