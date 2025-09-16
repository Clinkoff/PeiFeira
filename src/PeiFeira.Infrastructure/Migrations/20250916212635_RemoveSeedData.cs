using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeiFeira.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("9088e821-3661-43be-a874-57d24304beda"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "AlteradoEm", "CriadoEm", "DeletadoEm", "Email", "IsActive", "Matricula", "Nome", "Role", "SenhaHash" },
                values: new object[] { new Guid("9088e821-3661-43be-a874-57d24304beda"), null, new DateTime(2025, 9, 16, 21, 5, 6, 313, DateTimeKind.Utc).AddTicks(1408), null, "admin@univag.edu.br", true, "0000000001", "Administrador", 2, "admin123" });
        }
    }
}
