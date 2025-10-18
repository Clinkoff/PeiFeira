using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Turmas;

namespace PeiFeira.Infrastructure.Data.Configurations.Turmas;

public class AlunoTurmaConfiguration : IEntityTypeConfiguration<AlunoTurma>
{
    public void Configure(EntityTypeBuilder<AlunoTurma> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DataMatricula).IsRequired();
        builder.Property(e => e.IsAtual).IsRequired();

        builder.HasIndex(e => e.IsActive);
        builder.HasIndex(e => new { e.PerfilAlunoId, e.TurmaId, e.IsAtual });

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(at => at.PerfilAluno)
               .WithMany(pa => pa.Turmas)
               .HasForeignKey(at => at.PerfilAlunoId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(at => at.Turma)
               .WithMany(t => t.AlunosTurma)
               .HasForeignKey(at => at.TurmaId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
