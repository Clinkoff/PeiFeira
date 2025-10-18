using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.DisciplinasPI;

namespace PeiFeira.Infrastructure.Data.Configurations.DisciplinasPI;

public class DisciplinaPITurmaConfiguration : IEntityTypeConfiguration<DisciplinaPITurma>
{
    public void Configure(EntityTypeBuilder<DisciplinaPITurma> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => new { e.DisciplinaPIId, e.TurmaId })
               .IsUnique()
               .HasDatabaseName("IX_DisciplinaPITurma_Disciplina_Turma_Unique");

        builder.HasIndex(e => e.IsActive);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(dt => dt.DisciplinaPI)
               .WithMany(d => d.DisciplinaPITurmas)
               .HasForeignKey(dt => dt.DisciplinaPIId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dt => dt.Turma)
               .WithMany()
               .HasForeignKey(dt => dt.TurmaId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
