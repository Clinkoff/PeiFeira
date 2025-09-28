using Microsoft.EntityFrameworkCore.Storage;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

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

    // Repositórios de relacionamento
    private IMembroEquipeRepository? _membrosEquipe;
    private IConviteEquipeRepository? _convitesEquipe;
    private IPerfilAlunoRepository? _perfisAluno;
    private IPerfilProfessorRepository? _perfisProfessor;

    public UnitOfWork(PeiFeiraDbContext context)
    {
        _context = context;
    }

    // Lazy loading dos repositórios
    public IUsuarioRepository Usuarios =>
        _usuarios ??= new UsuarioRepository(_context);

    public IEquipeRepository Equipes => throw new NotImplementedException();

    public IProjetoRepository Projetos => throw new NotImplementedException();

    public IAvaliacaoRepository Avaliacoes => throw new NotImplementedException();

    public IMembroEquipeRepository MembrosEquipe => throw new NotImplementedException();

    public IConviteEquipeRepository ConvitesEquipe => throw new NotImplementedException();

    public IPerfilAlunoRepository PerfisAluno =>
        _perfisAluno ??= new PerfilAlunoRepository(_context);

    public IPerfilProfessorRepository PerfisProfessor =>
        _perfisProfessor ??= new PerfilProfessorRepository(_context);

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