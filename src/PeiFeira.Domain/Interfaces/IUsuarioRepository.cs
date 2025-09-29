using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Domain.Interfaces;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<Usuario?> GetByIdWithPerfilAsync(Guid id);
    Task<Usuario?> GetByMatriculaWithPerfilAsync(string matricula);
    Task<Usuario?> GetByMatriculaAsync (string matricula);
    Task<Usuario?> GetByEmailAsync (string email);
    Task<bool> ExistsByMatriculaAsync (string Matricula);
    Task<bool> ExistsByEmailAsync (string email);
    Task<IEnumerable<Usuario>> GetByRoleAsync(UserRole role);
    Task<IEnumerable<Usuario>> GetProfessoresAsync();              // Buscar só professores
    Task<IEnumerable<Usuario>> GetAlunosAsync();                   // Buscar só alunos
}
