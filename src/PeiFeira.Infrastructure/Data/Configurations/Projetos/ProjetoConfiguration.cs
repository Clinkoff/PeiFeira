using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Projetos;

namespace PeiFeira.Infrastructure.Data.Configurations.Projetos;

public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Titulo).HasMaxLength(200).IsRequired();
        builder.Property(e => e.DesafioProposto).HasMaxLength(2000).IsRequired();
        builder.Property(e => e.Status).HasConversion<int>().IsRequired();

        // Dados empresa/responsável
        builder.Property(e => e.NomeEmpresa).HasMaxLength(200);
        builder.Property(e => e.Cidade).HasMaxLength(100);
        builder.Property(e => e.NomeResponsavel).HasMaxLength(200);
        builder.Property(e => e.CargoResponsavel).HasMaxLength(100);
        builder.Property(e => e.EmailResponsavel).HasMaxLength(200);

        builder.HasIndex(e => e.IsActive);
        builder.HasIndex(e => e.DisciplinaPIId);
        builder.HasIndex(e => e.EquipeId);

        builder.HasIndex(e => new { e.DisciplinaPIId, e.EquipeId })
               .IsUnique()
               .HasDatabaseName("IX_Projeto_Disciplina_Equipe_Unique");

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(p => p.Equipe)
               .WithOne(e => e.Projeto)  // 1:1
               .HasForeignKey<Projeto>(p => p.EquipeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.DisciplinaPI)
               .WithMany(d => d.Projetos)
               .HasForeignKey(p => p.DisciplinaPIId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}