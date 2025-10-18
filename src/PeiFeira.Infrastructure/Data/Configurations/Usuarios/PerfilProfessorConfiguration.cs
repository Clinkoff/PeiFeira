using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Infrastructure.Data.Configurations.Usuarios;

public class PerfilProfessorConfiguration : IEntityTypeConfiguration<PerfilProfessor>
{
    public void Configure(EntityTypeBuilder<PerfilProfessor> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.UsuarioId).IsUnique();
        builder.HasIndex(e => e.IsActive);

        builder.Property(e => e.Departamento).HasMaxLength(200);
        builder.Property(e => e.AreaEspecializacao).HasMaxLength(300);
        builder.Property(e => e.Titulacao).HasMaxLength(100);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);
    }
}
