using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Turmas;

namespace PeiFeira.Infrastructure.Data.Configurations.Turmas;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Codigo).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Curso).HasMaxLength(200);
        builder.Property(e => e.Turno).HasMaxLength(20);

        builder.HasIndex(e => e.Codigo).IsUnique();
        builder.HasIndex(e => e.IsActive);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(t => t.Semestre)
               .WithMany(s => s.Turmas)
               .HasForeignKey(t => t.SemestreId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
