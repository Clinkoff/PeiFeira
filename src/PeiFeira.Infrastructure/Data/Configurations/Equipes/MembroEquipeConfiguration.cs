using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Equipes;

namespace PeiFeira.Infrastructure.Data.Configurations.Equipes;

public class MembroEquipeConfiguration : IEntityTypeConfiguration<MembroEquipe>
{
    public void Configure(EntityTypeBuilder<MembroEquipe> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Cargo).HasConversion<int>().IsRequired();
        builder.Property(e => e.Funcao).HasMaxLength(100);
        builder.Property(e => e.IngressouEm).IsRequired();
        builder.Property(e => e.SaiuEm);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(e => e.Equipe)
               .WithMany(eq => eq.Membros)
               .HasForeignKey(e => e.EquipeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PerfilAluno)
               .WithMany(pa => pa.MembroEquipes)
               .HasForeignKey(e => e.PerfilAlunoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.PerfilAlunoId, e.EquipeId, e.IsActive })
               .HasDatabaseName("IX_MembroEquipe_PerfilAluno_Equipe_Active");
        builder.HasIndex(e => e.IsActive);
    }
}
