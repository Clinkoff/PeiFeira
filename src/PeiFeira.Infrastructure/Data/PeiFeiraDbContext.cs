using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Entities.Semestres;
using PeiFeira.Domain.Entities.Turmas;
using PeiFeira.Domain.Entities.Usuarios;
using System.Linq.Expressions;

namespace PeiFeira.Infrastructure.Data;

public class PeiFeiraDbContext : DbContext
{
    public PeiFeiraDbContext(DbContextOptions<PeiFeiraDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<PerfilAluno> PerfisAluno { get; set; }
    public DbSet<PerfilProfessor> PerfisProfessor { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<MembroEquipe> MembrosEquipe { get; set; }
    public DbSet<ConviteEquipe> ConvitesEquipe { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Semestre> Semestres { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<AlunoTurma> AlunosTurma { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ===== USUARIO =====
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

            entity.HasOne(e => e.PerfilAluno)
                  .WithOne(pa => pa.Usuario)
                  .HasForeignKey<PerfilAluno>(pa => pa.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.PerfilProfessor)
                  .WithOne(p => p.Usuario)
                  .HasForeignKey<PerfilProfessor>(p => p.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ===== PERFIL ALUNO =====
        modelBuilder.Entity<PerfilAluno>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Curso).HasMaxLength(200);
            entity.Property(e => e.Periodo);

            entity.HasIndex(e => e.UsuarioId).IsUnique();
            entity.HasIndex(e => e.IsActive);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);
        });

        // ===== PERFIL PROFESSOR =====
        modelBuilder.Entity<PerfilProfessor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UsuarioId).IsUnique();
            entity.HasIndex(e => e.IsActive);

            entity.Property(e => e.Departamento).HasMaxLength(200);
            entity.Property(e => e.AreaEspecializacao).HasMaxLength(300);
            entity.Property(e => e.Titulacao).HasMaxLength(100);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);
        });

        // ===== SEMESTRE =====
        modelBuilder.Entity<Semestre>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).HasMaxLength(10).IsRequired();
            entity.Property(e => e.Ano).IsRequired();
            entity.Property(e => e.Periodo).IsRequired();
            entity.Property(e => e.DataInicio).IsRequired();
            entity.Property(e => e.DataFim).IsRequired();

            entity.HasIndex(e => new { e.Ano, e.Periodo }).IsUnique();
            entity.HasIndex(e => e.IsActive);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);
        });

        // ===== TURMA =====
        modelBuilder.Entity<Turma>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Codigo).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Curso).HasMaxLength(200);
            entity.Property(e => e.Turno).HasMaxLength(20);
            entity.Property(e => e.Tipo).HasConversion<int>();

            entity.HasIndex(e => e.Codigo).IsUnique();
            entity.HasIndex(e => e.IsActive);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            entity.HasOne(t => t.Semestre)
                  .WithMany(s => s.Turmas)
                  .HasForeignKey(t => t.SemestreId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ===== ALUNO_TURMA =====
        modelBuilder.Entity<AlunoTurma>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DataMatricula).IsRequired();
            entity.Property(e => e.IsAtual).IsRequired();

            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => new { e.PerfilAlunoId, e.TurmaId, e.IsAtual });

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            entity.HasOne(at => at.PerfilAluno)
                  .WithMany(pa => pa.Turmas)
                  .HasForeignKey(at => at.PerfilAlunoId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(at => at.Turma)
                  .WithMany(t => t.AlunosTurma)
                  .HasForeignKey(at => at.TurmaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ===== PROJETO =====
        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Projeto");

            entity.Property(e => e.Titulo).HasMaxLength(300).IsRequired();
            entity.Property(e => e.Tema).HasMaxLength(200).IsRequired();
            entity.Property(e => e.DesafioProposto).HasMaxLength(2000).IsRequired();
            entity.Property(e => e.Status).HasConversion<int>().IsRequired();

            entity.Property(e => e.NomeEmpresa).HasMaxLength(200);
            entity.Property(e => e.EnderecoCompleto).HasMaxLength(500);
            entity.Property(e => e.Cidade).HasMaxLength(100);
            entity.Property(e => e.RedeSocial).HasMaxLength(200);
            entity.Property(e => e.Contato).HasMaxLength(100);

            entity.Property(e => e.NomeResponsavel).HasMaxLength(200);
            entity.Property(e => e.CargoResponsavel).HasMaxLength(100);
            entity.Property(e => e.TelefoneResponsavel).HasMaxLength(20);
            entity.Property(e => e.EmailResponsavel).HasMaxLength(200);
            entity.Property(e => e.RedesSociaisResponsavel).HasMaxLength(500);

            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.SemestreId);
            entity.HasIndex(e => e.TurmaId);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            entity.HasOne(p => p.Semestre)
                  .WithMany(s => s.Projetos)
                  .HasForeignKey(p => p.SemestreId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Turma)
                  .WithMany(t => t.Projetos)
                  .HasForeignKey(p => p.TurmaId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.ProfessorOrientador)
                  .WithMany(pp => pp.ProjetosOrientados)
                  .HasForeignKey(p => p.PerfilProfessorOrientadorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(p => p.Equipes)
                  .WithOne(e => e.Projeto)
                  .HasForeignKey(e => e.ProjetoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ===== EQUIPE =====
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
                  .HasForeignKey(e => e.LiderPerfilAlunoId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.CodigoConvite).IsUnique();
            entity.HasIndex(e => e.LiderPerfilAlunoId);
            entity.HasIndex(e => e.IsActive);
        });

        // ===== CONVITE_EQUIPE =====
        modelBuilder.Entity<ConviteEquipe>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.Mensagem).HasMaxLength(500);
            entity.Property(e => e.MotivoResposta).HasMaxLength(500);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

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

        // ===== MEMBRO_EQUIPE =====
        modelBuilder.Entity<MembroEquipe>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Cargo).HasConversion<int>().IsRequired();
            entity.Property(e => e.Funcao).HasMaxLength(100);
            entity.Property(e => e.IngressouEm).IsRequired();
            entity.Property(e => e.SaiuEm);

            entity.Property(e => e.CriadoEm).IsRequired();
            entity.Property(e => e.AlteradoEm);
            entity.Property(e => e.DeletadoEm);

            entity.HasOne(e => e.Equipe)
                  .WithMany(eq => eq.Membros)
                  .HasForeignKey(e => e.EquipeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.PerfilAluno)
                  .WithMany(pa => pa.MembroEquipes)
                  .HasForeignKey(e => e.PerfilAlunoId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.PerfilAlunoId, e.EquipeId, e.IsActive })
                  .HasDatabaseName("IX_MembroEquipe_PerfilAluno_Equipe_Active");
            entity.HasIndex(e => e.IsActive);
        });

        // ===== AVALIACAO =====
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

            entity.Property(e => e.PontuacaoTotal).HasPrecision(5, 2);
            entity.Property(e => e.NotaFinal).HasPrecision(4, 2);
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
                    entry.Property(nameof(Auditable.CriadoEm)).IsModified = false;
                    break;

                case EntityState.Deleted:
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