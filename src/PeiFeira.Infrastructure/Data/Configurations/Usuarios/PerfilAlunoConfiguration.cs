using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Infrastructure.Data.Configurations.Usuarios;

public class PerfilAlunoConfiguration : IEntityTypeConfiguration<PerfilAluno>
{
    public void Configure(EntityTypeBuilder<PerfilAluno> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Curso).HasMaxLength(200);
        builder.Property(e => e.Turno).HasMaxLength(50);

        builder.HasIndex(e => e.UsuarioId).IsUnique();
        builder.HasIndex(e => e.IsActive);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);
    }
}
