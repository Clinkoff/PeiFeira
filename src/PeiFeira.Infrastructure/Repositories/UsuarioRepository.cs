using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> GetByMatriculaAsync(string matricula)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Matricula == matricula);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByMatriculaAsync(string matricula)
    {
        return await _dbSet.AnyAsync(u => u.Matricula == matricula);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<Usuario>> GetByRoleAsync(UserRole role)
    {
        return await _dbSet.Where(u => u.Role == role).ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> GetProfessoresAsync()
    {
        return await _dbSet.Where(u => u.Role == UserRole.Professor).ToListAsync();
    }


    public async Task<IEnumerable<Usuario>> GetAlunosAsync()
    {
        return await _dbSet.Where(u => u.Role == UserRole.Aluno).ToListAsync();
    }
}