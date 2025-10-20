using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Equipes;

namespace PeiFeira.Infrastructure.Data.Configurations.Equipes;

public class EquipeConfiguration : IEntityTypeConfiguration<Equipe>
{
    public void Configure(EntityTypeBuilder<Equipe> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome).HasMaxLength(200).IsRequired();
        builder.Property(e => e.UrlQrCode).HasMaxLength(500);
        builder.Property(e => e.CodigoConvite).HasMaxLength(50);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(e => e.Lider)
               .WithMany()
               .HasForeignKey(e => e.LiderPerfilAlunoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.CodigoConvite).IsUnique();
        builder.HasIndex(e => e.LiderPerfilAlunoId);
        builder.HasIndex(e => e.IsActive);
    }
}
