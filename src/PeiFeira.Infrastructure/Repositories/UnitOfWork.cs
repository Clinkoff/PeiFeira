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

    //public IEquipeRepository Equipes =>
    //    _equipes ??= new EquipeRepository(_context);

    //public IProjetoRepository Projetos =>
    //    _projetos ??= new ProjetoRepository(_context);

    //public IAvaliacaoRepository Avaliacoes =>
    //    _avaliacoes ??= new AvaliacaoRepository(_context);

    //public IMembroEquipeRepository MembrosEquipe =>
    //    _membrosEquipe ??= new MembroEquipeRepository(_context);

    //  public IConviteEquipeRepository ConvitesEquipe =>
    //    _convitesEquipe ??= new ConviteEquipeRepository(_context);

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