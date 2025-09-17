using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Convites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConvitesEquipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConvidadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConvidadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ConvitesEquipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConvitesEquipe_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConvitesEquipe_Usuarios_ConvidadoId",
                        column: x => x.ConvidadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConvitesEquipe_Usuarios_ConvidadoPorId",
                        column: x => x.ConvidadoPorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConviteEquipe_Equipe_Convidado_Unique",
                table: "ConvitesEquipe",
                columns: new[] { "EquipeId", "ConvidadoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConvitesEquipe_ConvidadoId",
                table: "ConvitesEquipe",
                column: "ConvidadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConvitesEquipe_ConvidadoPorId",
                table: "ConvitesEquipe",
                column: "ConvidadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_ConvitesEquipe_Status",
                table: "ConvitesEquipe",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConvitesEquipe");
        }
    }
}
