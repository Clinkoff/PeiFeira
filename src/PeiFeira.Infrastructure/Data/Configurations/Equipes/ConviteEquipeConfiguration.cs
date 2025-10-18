using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Equipes;

namespace PeiFeira.Infrastructure.Data.Configurations.Equipes;

public class ConviteEquipeConfiguration : IEntityTypeConfiguration<ConviteEquipe>
{
    public void Configure(EntityTypeBuilder<ConviteEquipe> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Status).HasConversion<int>();
        builder.Property(e => e.Mensagem).HasMaxLength(500);
        builder.Property(e => e.MotivoResposta).HasMaxLength(500);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(e => e.Equipe)
               .WithMany()
               .HasForeignKey(e => e.EquipeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ConvidadoPor)
               .WithMany()
               .HasForeignKey(e => e.ConvidadoPorPerfilAlunoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Convidado)
               .WithMany()
               .HasForeignKey(e => e.ConvidadoPerfilAlunoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.EquipeId, e.ConvidadoPerfilAlunoId })
               .IsUnique()
               .HasDatabaseName("IX_ConviteEquipe_Equipe_Convidado_Unique");

        builder.HasIndex(e => e.ConvidadoPerfilAlunoId);
        builder.HasIndex(e => e.Status);
    }
}
