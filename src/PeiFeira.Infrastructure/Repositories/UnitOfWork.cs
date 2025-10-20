using Microsoft.EntityFrameworkCore.Storage;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Domain.Interfaces.DisciplinasPI;
using PeiFeira.Domain.Interfaces.Usuarios;
using PeiFeira.Domain.Interfaces.Equipes;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Infrastructure.Data;
using PeiFeira.Domain.Interfaces.Avaliacoes;
using PeiFeira.Domain.Interfaces.Projetos;
using PeiFeira.Domain.Interfaces.Semestres;
using PeiFeira.Domain.Interfaces.Turmas;

namespace PeiFeira.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PeiFeiraDbContext _context;
    private IDbContextTransaction? _transaction;

    // Repositórios principais
    private IUsuarioRepository? _usuarios;
    private IEquipeRepository? _equipes;
    private IProjetoRepository? _projetos;
    private IAvaliacaoRepository? _avaliacoes;
    private ISemestre? _semestres;
    private ITurma? _turmas;

    // Repositórios de relacionamento
    private IMembroEquipeRepository? _membrosEquipe;
    private IConviteEquipeRepository? _convitesEquipe;
    private IPerfilAlunoRepository? _perfisAluno;
    private IPerfilProfessorRepository? _perfisProfessor;
    private IAlunoTurmaRepository? _alunoTurmas;

    // Repositórios de Disciplina PI
    private IDisciplinaPIRepository? _disciplinasPI;
    private IDisciplinaPITurmaRepository? _disciplinaPITurmas;

    public UnitOfWork(PeiFeiraDbContext context)
    {
        _context = context;
    }

    // Lazy loading dos repositórios
    public IUsuarioRepository Usuarios =>
        _usuarios ??= new UsuarioRepository(_context);

    public IEquipeRepository Equipes =>
        _equipes ??= new EquipeRepository(_context);

    public IProjetoRepository Projetos =>
        _projetos ??= new ProjetoRepository(_context);

    public IAvaliacaoRepository Avaliacoes =>
        _avaliacoes ??= new AvaliacaoRepository(_context);

    public IMembroEquipeRepository MembrosEquipe =>
        _membrosEquipe ??= new MembroEquipeRepository(_context);

    public IConviteEquipeRepository ConvitesEquipe =>
        _convitesEquipe ??= new ConviteEquipeRepository(_context);
   
    public IPerfilAlunoRepository PerfisAluno =>
        _perfisAluno ??= new PerfilAlunoRepository(_context);

    public IPerfilProfessorRepository PerfisProfessor =>
        _perfisProfessor ??= new PerfilProfessorRepository(_context);

    public IDisciplinaPIRepository DisciplinasPI =>
        _disciplinasPI ??= new DisciplinaPIRepository(_context);

    public IDisciplinaPITurmaRepository DisciplinaPITurmas =>
        _disciplinaPITurmas ??= new DisciplinaPITurmaRepository(_context);

    public ISemestre Semestres =>
       _semestres ??= new SemestreRepository(_context);

    public ITurma Turmas =>
        _turmas ??= new TurmaRepository(_context);
    public IAlunoTurmaRepository AlunoTurmas =>
        _alunoTurmas ??= new AlunoTurmaRepository(_context);

    // Controle de transações
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
            throw new InvalidOperationException("Uma transação já está em andamento");

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("Nenhuma transação em andamento");

        try
        {
            await _transaction.CommitAsync();
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("Nenhuma transação em andamento");

        try
        {
            await _transaction.RollbackAsync();
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}