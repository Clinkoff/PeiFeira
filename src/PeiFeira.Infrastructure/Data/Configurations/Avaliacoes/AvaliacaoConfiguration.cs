using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Avaliacoes;

namespace PeiFeira.Infrastructure.Data.Configurations.Avaliacoes;

public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.RelevanciaProblema).IsRequired();
        builder.Property(e => e.FundamentacaoProblema).IsRequired();
        builder.Property(e => e.FocoSolucao).IsRequired();
        builder.Property(e => e.ViabilidadeSolucao).IsRequired();
        builder.Property(e => e.ClarezaApresentacao).IsRequired();
        builder.Property(e => e.DominioAssunto).IsRequired();
        builder.Property(e => e.TransmissaoInformacoes).IsRequired();
        builder.Property(e => e.PadronizacaoApresentacao).IsRequired();
        builder.Property(e => e.LinguagemTempo).IsRequired();
        builder.Property(e => e.QualidadeRespostas).IsRequired();

        builder.Property(e => e.PontuacaoTotal).HasPrecision(5, 2);
        builder.Property(e => e.NotaFinal).HasPrecision(4, 2);
        builder.Property(e => e.Comentarios).HasMaxLength(1000);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(a => a.Avaliador)
               .WithMany(u => u.Avaliacoes)
               .HasForeignKey(a => a.AvaliadorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Equipe)
               .WithMany(e => e.Avaliacoes)
               .HasForeignKey(a => a.EquipeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.EquipeId, e.AvaliadorId })
               .IsUnique()
               .HasDatabaseName("IX_Avaliacao_Equipe_Avaliador_Unique");
        builder.HasIndex(e => e.AvaliadorId);
        builder.HasIndex(e => e.NotaFinal);
        builder.HasIndex(e => e.IsActive);
    }
}
