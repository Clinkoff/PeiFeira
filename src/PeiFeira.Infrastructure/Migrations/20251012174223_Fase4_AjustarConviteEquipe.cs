using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fase4_AjustarConviteEquipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConviteEquipe_Usuario_ConvidadoId",
                table: "ConviteEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ConviteEquipe_Usuario_ConvidadoPorId",
                table: "ConviteEquipe");

            migrationBuilder.RenameColumn(
                name: "ConvidadoPorId",
                table: "ConviteEquipe",
                newName: "ConvidadoPorPerfilAlunoId");

            migrationBuilder.RenameColumn(
                name: "ConvidadoId",
                table: "ConviteEquipe",
                newName: "ConvidadoPerfilAlunoId");

            migrationBuilder.RenameIndex(
                name: "IX_ConviteEquipe_ConvidadoPorId",
                table: "ConviteEquipe",
                newName: "IX_ConviteEquipe_ConvidadoPorPerfilAlunoId");

            migrationBuilder.RenameIndex(
                name: "IX_ConviteEquipe_ConvidadoId",
                table: "ConviteEquipe",
                newName: "IX_ConviteEquipe_ConvidadoPerfilAlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConviteEquipe_PerfilAluno_ConvidadoPerfilAlunoId",
                table: "ConviteEquipe",
                column: "ConvidadoPerfilAlunoId",
                principalTable: "PerfilAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConviteEquipe_PerfilAluno_ConvidadoPorPerfilAlunoId",
                table: "ConviteEquipe",
                column: "ConvidadoPorPerfilAlunoId",
                principalTable: "PerfilAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConviteEquipe_PerfilAluno_ConvidadoPerfilAlunoId",
                table: "ConviteEquipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ConviteEquipe_PerfilAluno_ConvidadoPorPerfilAlunoId",
                table: "ConviteEquipe");

            migrationBuilder.RenameColumn(
                name: "ConvidadoPorPerfilAlunoId",
                table: "ConviteEquipe",
                newName: "ConvidadoPorId");

            migrationBuilder.RenameColumn(
                name: "ConvidadoPerfilAlunoId",
                table: "ConviteEquipe",
                newName: "ConvidadoId");

            migrationBuilder.RenameIndex(
                name: "IX_ConviteEquipe_ConvidadoPorPerfilAlunoId",
                table: "ConviteEquipe",
                newName: "IX_ConviteEquipe_ConvidadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_ConviteEquipe_ConvidadoPerfilAlunoId",
                table: "ConviteEquipe",
                newName: "IX_ConviteEquipe_ConvidadoId");

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
        }
    }
}
