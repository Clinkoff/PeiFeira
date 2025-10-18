using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Projetos;

namespace PeiFeira.Infrastructure.Data.Configurations.Projetos;

public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("Projeto");

        builder.Property(e => e.Titulo).HasMaxLength(300).IsRequired();
        builder.Property(e => e.DesafioProposto).HasMaxLength(2000).IsRequired();
        builder.Property(e => e.Status).HasConversion<int>().IsRequired();

        builder.Property(e => e.NomeEmpresa).HasMaxLength(200);
        builder.Property(e => e.EnderecoCompleto).HasMaxLength(500);
        builder.Property(e => e.Cidade).HasMaxLength(100);
        builder.Property(e => e.RedeSocial).HasMaxLength(200);
        builder.Property(e => e.Contato).HasMaxLength(100);

        builder.Property(e => e.NomeResponsavel).HasMaxLength(200);
        builder.Property(e => e.CargoResponsavel).HasMaxLength(100);
        builder.Property(e => e.TelefoneResponsavel).HasMaxLength(20);
        builder.Property(e => e.EmailResponsavel).HasMaxLength(200);
        builder.Property(e => e.RedesSociaisResponsavel).HasMaxLength(500);

        builder.HasIndex(e => e.IsActive);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.DisciplinaPIId);

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasOne(p => p.DisciplinaPI)
               .WithMany(d => d.Projetos)
               .HasForeignKey(p => p.DisciplinaPIId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
