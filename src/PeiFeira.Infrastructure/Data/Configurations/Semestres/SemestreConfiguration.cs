using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Semestres;

namespace PeiFeira.Infrastructure.Data.Configurations.Semestres;

public class SemestreConfiguration : IEntityTypeConfiguration<Semestre>
{
    public void Configure(EntityTypeBuilder<Semestre> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome).HasMaxLength(10).IsRequired();
        builder.Property(e => e.Ano).IsRequired();
        builder.Property(e => e.Periodo).IsRequired();
        builder.Property(e => e.DataInicio).IsRequired();
        builder.Property(e => e.DataFim).IsRequired();

        builder.HasIndex(e => new { e.Ano, e.Periodo }).IsUnique();
        builder.HasIndex(e => e.IsActive);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);
    }
}
