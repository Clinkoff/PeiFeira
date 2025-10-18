using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.DisciplinasPI;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Entities.Semestres;
using PeiFeira.Domain.Entities.Turmas;
using PeiFeira.Domain.Entities.Usuarios;
using System.Linq.Expressions;
using System.Reflection;

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
    public DbSet<DisciplinaPI> DisciplinasPI { get; set; }
    public DbSet<DisciplinaPITurma> DisciplinasPITurma { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica todas as configurações do assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Aplica filtros globais
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