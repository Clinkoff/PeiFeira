using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeiFeira.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Matricula).HasMaxLength(20).IsRequired();
        builder.Property(e => e.Nome).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(255).IsRequired();
        builder.Property(e => e.SenhaHash).HasMaxLength(500).IsRequired();
        builder.Property(e => e.Role).HasConversion<int>();

        builder.Property(e => e.CriadoEm).IsRequired();
        builder.Property(e => e.AlteradoEm);
        builder.Property(e => e.DeletadoEm);

        builder.HasIndex(e => e.Matricula).IsUnique();
        builder.HasIndex(e => e.Email).IsUnique();
        builder.HasIndex(e => e.IsActive);

        builder.HasOne(e => e.PerfilAluno)
               .WithOne(pa => pa.Usuario)
               .HasForeignKey<PerfilAluno>(pa => pa.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PerfilProfessor)
               .WithOne(p => p.Usuario)
               .HasForeignKey<PerfilProfessor>(p => p.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
