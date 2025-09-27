using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPerfilAlunoPerfilProfessor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Equipes_EquipeId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Usuarios_AvaliadorId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvitesEquipe_Equipes_EquipeId",
                table: "ConvitesEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvitesEquipe_Usuarios_ConvidadoId",
                table: "ConvitesEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvitesEquipe_Usuarios_ConvidadoPorId",
                table: "ConvitesEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipes_Usuarios_LiderId",
                table: "Equipes");

            migrationBuilder.DropForeignKey(
                name: "FK_MembrosEquipe_Equipes_EquipeId",
                table: "MembrosEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_MembrosEquipe_Usuarios_UsuarioId",
                table: "MembrosEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Equipes_EquipeId",
                table: "Projetos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projetos",
                table: "Projetos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembrosEquipe",
                table: "MembrosEquipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipes",
                table: "Equipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConvitesEquipe",
                table: "ConvitesEquipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Projetos",
                newName: "Projeto");

            migrationBuilder.RenameTable(
                name: "MembrosEquipe",
                newName: "MembroEquipe");

            migrationBuilder.RenameTable(
                name: "Equipes",
                newName: "Equipe");

            migrationBuilder.RenameTable(
                name: "ConvitesEquipe",
                newName: "ConviteEquipe");

            migrationBuilder.RenameTable(
                name: "Avaliacoes",
                newName: "Avaliacao");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Matricula",
                table: "Usuario",
                newName: "IX_Usuario_Matricula");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_IsActive",
                table: "Usuario",
                newName: "IX_Usuario_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Email",
                table: "Usuario",
                newName: "IX_Usuario_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Projetos_IsActive",
                table: "Projeto",
                newName: "IX_Projeto_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Projetos_EquipeId",
                table: "Projeto",
                newName: "IX_Projeto_EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_MembrosEquipe_IsActive",
                table: "MembroEquipe",
                newName: "IX_MembroEquipe_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_MembrosEquipe_EquipeId",
                table: "MembroEquipe",
                newName: "IX_MembroEquipe_EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipes_LiderId",
                table: "Equipe",
                newName: "IX_Equipe_LiderId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipes_IsActive",
                table: "Equipe",
                newName: "IX_Equipe_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Equipes_CodigoConvite",
                table: "Equipe",
                newName: "IX_Equipe_CodigoConvite");

            migrationBuilder.RenameIndex(
                name: "IX_ConvitesEquipe_Status",
                table: "ConviteEquipe",
                newName: "IX_ConviteEquipe_Status");

            migrationBuilder.RenameIndex(
                name: "IX_ConvitesEquipe_ConvidadoPorId",
                table: "ConviteEquipe",
                newName: "IX_ConviteEquipe_ConvidadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_ConvitesEquipe_ConvidadoId",
                table: "ConviteEquipe",
                newName: "IX_ConviteEquipe_ConvidadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_NotaFinal",
                table: "Avaliacao",
                newName: "IX_Avaliacao_NotaFinal");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_IsActive",
                table: "Avaliacao",
                newName: "IX_Avaliacao_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_AvaliadorId",
                table: "Avaliacao",
                newName: "IX_Avaliacao_AvaliadorId");

            migrationBuilder.AddColumn<Guid>(
                name: "PerfilProfessorId",
                table: "Projeto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PerfilAlunoId",
                table: "MembroEquipe",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projeto",
                table: "Projeto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembroEquipe",
                table: "MembroEquipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipe",
                table: "Equipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConviteEquipe",
                table: "ConviteEquipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avaliacao",
                table: "Avaliacao",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PefilAluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Curso = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Periodo = table.Column<int>(type: "int", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PefilAluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PefilAluno_Usuario_UsuarioId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_PerfilProfessorId",
                table: "Projeto",
                column: "PerfilProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEquipe_PerfilAlunoId",
                table: "MembroEquipe",
                column: "PerfilAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_PefilAluno_IsActive",
                table: "PefilAluno",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_PefilAluno_UsuarioId",
                table: "PefilAluno",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_Equipe_EquipeId",
                table: "Avaliacao",
                column: "EquipeId",
                principalTable: "Equipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_Usuario_AvaliadorId",
                table: "Avaliacao",
                column: "AvaliadorId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConviteEquipe_Equipe_EquipeId",
                table: "ConviteEquipe",
                column: "EquipeId",
                principalTable: "Equipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConviteEquipe_Usuario_ConvidadoId",
                table: "ConviteEquipe",
                column: "ConvidadoId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConviteEquipe_Usuario_ConvidadoPorId",
                table: "ConviteEquipe",
                column: "ConvidadoPorId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipe_Usuario_LiderId",
                table: "Equipe",
                column: "LiderId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MembroEquipe_Equipe_EquipeId",
                table: "MembroEquipe",
                column: "EquipeId",
                principalTable: "Equipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Equipe_EquipeId",
                table: "Avaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Usuario_AvaliadorId",
                table: "Avaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_ConviteEquipe_Equipe_EquipeId",
                table: "ConviteEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ConviteEquipe_Usuario_ConvidadoId",
                table: "ConviteEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ConviteEquipe_Usuario_ConvidadoPorId",
                table: "ConviteEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipe_Usuario_LiderId",
                table: "Equipe");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroEquipe_Equipe_EquipeId",
                table: "MembroEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroEquipe_PefilAluno_PerfilAlunoId",
                table: "MembroEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroEquipe_Usuario_UsuarioId",
                table: "MembroEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_Equipe_EquipeId",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_PerfilProfessor_PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropTable(
                name: "PefilAluno");

            migrationBuilder.DropTable(
                name: "PerfilProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projeto",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_Projeto_PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembroEquipe",
                table: "MembroEquipe");

            migrationBuilder.DropIndex(
                name: "IX_MembroEquipe_PerfilAlunoId",
                table: "MembroEquipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipe",
                table: "Equipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConviteEquipe",
                table: "ConviteEquipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avaliacao",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "PerfilProfessorId",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "PerfilAlunoId",
                table: "MembroEquipe");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Projeto",
                newName: "Projetos");

            migrationBuilder.RenameTable(
                name: "MembroEquipe",
                newName: "MembrosEquipe");

            migrationBuilder.RenameTable(
                name: "Equipe",
                newName: "Equipes");

            migrationBuilder.RenameTable(
                name: "ConviteEquipe",
                newName: "ConvitesEquipe");

            migrationBuilder.RenameTable(
                name: "Avaliacao",
                newName: "Avaliacoes");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_Matricula",
                table: "Usuarios",
                newName: "IX_Usuarios_Matricula");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_IsActive",
                table: "Usuarios",
                newName: "IX_Usuarios_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_Email",
                table: "Usuarios",
                newName: "IX_Usuarios_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Projeto_IsActive",
                table: "Projetos",
                newName: "IX_Projetos_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Projeto_EquipeId",
                table: "Projetos",
                newName: "IX_Projetos_EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_MembroEquipe_IsActive",
                table: "MembrosEquipe",
                newName: "IX_MembrosEquipe_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_MembroEquipe_EquipeId",
                table: "MembrosEquipe",
                newName: "IX_MembrosEquipe_EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipe_LiderId",
                table: "Equipes",
                newName: "IX_Equipes_LiderId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipe_IsActive",
                table: "Equipes",
                newName: "IX_Equipes_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Equipe_CodigoConvite",
                table: "Equipes",
                newName: "IX_Equipes_CodigoConvite");

            migrationBuilder.RenameIndex(
                name: "IX_ConviteEquipe_Status",
                table: "ConvitesEquipe",
                newName: "IX_ConvitesEquipe_Status");

            migrationBuilder.RenameIndex(
                name: "IX_ConviteEquipe_ConvidadoPorId",
                table: "ConvitesEquipe",
                newName: "IX_ConvitesEquipe_ConvidadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_ConviteEquipe_ConvidadoId",
                table: "ConvitesEquipe",
                newName: "IX_ConvitesEquipe_ConvidadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacao_NotaFinal",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_NotaFinal");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacao_IsActive",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacao_AvaliadorId",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_AvaliadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projetos",
                table: "Projetos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembrosEquipe",
                table: "MembrosEquipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipes",
                table: "Equipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConvitesEquipe",
                table: "ConvitesEquipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Equipes_EquipeId",
                table: "Avaliacoes",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Usuarios_AvaliadorId",
                table: "Avaliacoes",
                column: "AvaliadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvitesEquipe_Equipes_EquipeId",
                table: "ConvitesEquipe",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvitesEquipe_Usuarios_ConvidadoId",
                table: "ConvitesEquipe",
                column: "ConvidadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvitesEquipe_Usuarios_ConvidadoPorId",
                table: "ConvitesEquipe",
                column: "ConvidadoPorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipes_Usuarios_LiderId",
                table: "Equipes",
                column: "LiderId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MembrosEquipe_Equipes_EquipeId",
                table: "MembrosEquipe",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembrosEquipe_Usuarios_UsuarioId",
                table: "MembrosEquipe",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Equipes_EquipeId",
                table: "Projetos",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
