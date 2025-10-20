using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.DisciplinasPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Infrastructure.Data.Configurations.DisciplinasPI;

public class DisciplinaPIConfiguration : IEntityTypeConfiguration<DisciplinaPI>
{
    public void Configure(EntityTypeBuilder<DisciplinaPI> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Nome).HasMaxLength(200).IsRequired();
        builder.Property(e => e.TemaGeral).HasMaxLength(500).IsRequired();
        builder.Property(e => e.Descricao).HasMaxLength(2000);
        builder.Property(e => e.Objetivos).HasMaxLength(2000);
        builder.Property(e => e.Status).HasConversion<int>().IsRequired();

        builder.HasIndex(e => e.IsActive);
        builder.HasIndex(e => e.SemestreId);
        builder.HasIndex(e => e.PerfilProfessorId);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(d => d.Semestre)
              .WithMany()
              .HasForeignKey(d => d.SemestreId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Professor)
          .WithMany(p => p.DisciplinasProfessor) 
          .HasForeignKey(d => d.PerfilProfessorId)
          .OnDelete(DeleteBehavior.Restrict);
    }
}
