using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Entities.Usuarios;
using System.Linq.Expressions;

namespace PeiFeira.Infrastructure.Data;

public class PeiFeiraDbContext : DbContext
{
    public PeiFeiraDbContext(DbContextOptions<PeiFeiraDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<MembroEquipe> MembrosEquipe { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<ConviteEquipe> ConvitesEquipe { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Matricula).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Nome).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
            entity.Property(e => e.SenhaHash).HasMaxLength(500).IsRequired(); 
            entity.Property(e => e.Role).HasConversion<int>();

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            entity.HasIndex(e => e.Matricula).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasIndex(e => e.IsActive);
        });

        // Configuração 
        modelBuilder.Entity<Equipe>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).HasMaxLength(200).IsRequired(); 
            entity.Property(e => e.UrlQrCode).HasMaxLength(500);
            entity.Property(e => e.CodigoConvite).HasMaxLength(50);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            entity.HasOne(e => e.Lider)
                  .WithMany()
                  .HasForeignKey(e => e.LiderId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.CodigoConvite).IsUnique();
            entity.HasIndex(e => e.LiderId);
            entity.HasIndex(e => e.IsActive);
        });

        modelBuilder.Entity<ConviteEquipe>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.Mensagem).HasMaxLength(500);
            entity.Property(e => e.MotivoResposta).HasMaxLength(500);

            entity.HasOne(e => e.Equipe)
                  .WithMany()
                  .HasForeignKey(e => e.EquipeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.ConvidadoPor)
                  .WithMany()
                  .HasForeignKey(e => e.ConvidadoPorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Convidado)
                  .WithMany()
                  .HasForeignKey(e => e.ConvidadoId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.EquipeId, e.ConvidadoId })
                  .IsUnique()
                  .HasDatabaseName("IX_ConviteEquipe_Equipe_Convidado_Unique");

            entity.HasIndex(e => e.ConvidadoId);
            entity.HasIndex(e => e.Status);
        });

        // Configuração 
        modelBuilder.Entity<MembroEquipe>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Funcao).HasConversion<int>();
            entity.Property(e => e.IngressouEm).IsRequired();
            entity.Property(e => e.SaiuEm);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            entity.HasOne(e => e.Usuario)
                  .WithMany(u => u.MembroEquipes)
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Equipe)
                  .WithMany(eq => eq.Membros)
                  .HasForeignKey(e => e.EquipeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.UsuarioId, e.EquipeId, e.IsActive })
                  .HasDatabaseName("IX_MembroEquipe_Usuario_Equipe_Active");
            entity.HasIndex(e => e.IsActive);
        });

        // Configuração Projeto
        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).HasMaxLength(300).IsRequired(); 
            entity.Property(e => e.Tema).HasMaxLength(500).IsRequired(); 
            entity.Property(e => e.DesafioProposto).HasMaxLength(2000).IsRequired(); 

            entity.Property(e => e.NomeEmpresa).HasMaxLength(300);
            entity.Property(e => e.EnderecoCompleto).HasMaxLength(500);
            entity.Property(e => e.Cidade).HasMaxLength(100);
            entity.Property(e => e.RedeSocial).HasMaxLength(300);
            entity.Property(e => e.Contato).HasMaxLength(200);

            entity.Property(e => e.NomeResponsavel).HasMaxLength(200);
            entity.Property(e => e.CargoResponsavel).HasMaxLength(150);
            entity.Property(e => e.TelefoneResponsavel).HasMaxLength(20);
            entity.Property(e => e.EmailResponsavel).HasMaxLength(255);
            entity.Property(e => e.RedesSociaisResponsavel).HasMaxLength(300);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            // Relacionamento 1:1 com Equipe
            entity.HasOne(p => p.Equipe)
                  .WithOne(e => e.Projeto)
                  .HasForeignKey<Projeto>(p => p.EquipeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.EquipeId).IsUnique();
            entity.HasIndex(e => e.IsActive);
        });

        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.RelevanciaProblema).IsRequired();
            entity.Property(e => e.FundamentacaoProblema).IsRequired();
            entity.Property(e => e.FocoSolucao).IsRequired();
            entity.Property(e => e.ViabilidadeSolucao).IsRequired();
            entity.Property(e => e.ClarezaApresentacao).IsRequired();
            entity.Property(e => e.DominioAssunto).IsRequired();
            entity.Property(e => e.TransmissaoInformacoes).IsRequired();
            entity.Property(e => e.PadronizacaoApresentacao).IsRequired();
            entity.Property(e => e.LinguagemTempo).IsRequired();
            entity.Property(e => e.QualidadeRespostas).IsRequired();

            entity.Property(e => e.PontuacaoTotal)
                  .HasPrecision(5, 2) // 999.99 (5 dígitos total, 2 decimais)
                  .IsRequired();

            entity.Property(e => e.NotaFinal)
                  .HasPrecision(4, 2) // 99.99 (4 dígitos total, 2 decimais)
                  .IsRequired();

            entity.Property(e => e.Comentarios).HasMaxLength(1000);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            entity.HasOne(a => a.Avaliador)
                  .WithMany(u => u.Avaliacoes)
                  .HasForeignKey(a => a.AvaliadorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Equipe)
                  .WithMany(e => e.Avaliacoes)
                  .HasForeignKey(a => a.EquipeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.EquipeId, e.AvaliadorId })
                  .IsUnique()
                  .HasDatabaseName("IX_Avaliacao_Equipe_Avaliador_Unique");
            entity.HasIndex(e => e.AvaliadorId);
            entity.HasIndex(e => e.NotaFinal);
            entity.HasIndex(e => e.IsActive);
        });

        ApplyGlobalQueryFilters(modelBuilder);

    }

    private static void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var isActiveProperty = entityType.FindProperty("IsActive");
            if (isActiveProperty != null && isActiveProperty.ClrType == typeof(bool))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, "IsActive");
                var condition = Expression.Equal(property, Expression.Constant(true));
                var lambda = Expression.Lambda(condition, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }

    public override int SaveChanges()
    {
        UpdateAuditableEntities();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Auditable &&
                       (e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted));

        foreach (var entry in entries)
        {
            var auditable = (Auditable)entry.Entity;
            var now = DateTime.UtcNow;

            switch (entry.State)
            {
                case EntityState.Added:
                    auditable.CriadoEm = now;
                    break;

                case EntityState.Modified:
                    auditable.AlteradoEm = now;
                    // Prevenir mudança de CriadoEm
                    entry.Property(nameof(Auditable.CriadoEm)).IsModified = false;
                    break;

                case EntityState.Deleted:
                    // Soft delete
                    if (entry.Entity is IBaseEntity baseEntity)
                    {
                        entry.State = EntityState.Modified;
                        baseEntity.IsActive = false;
                        auditable.DeletadoEm = now;
                        auditable.AlteradoEm = now;
                    }
                    break;
            }
        }
    }

    public IQueryable<T> GetAllIncludingInactive<T>() where T : class
    {
        return Set<T>().IgnoreQueryFilters();
    }

    public IQueryable<T> GetAllAsNoTracking<T>() where T : class
    {
        return Set<T>().AsNoTracking();
    }
}