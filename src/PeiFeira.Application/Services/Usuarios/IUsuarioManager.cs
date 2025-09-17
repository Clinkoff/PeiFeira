using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Communication.Responses.Usuario;

namespace PeiFeira.Application.Services.Usuarios;

public interface IUsuarioManager
{
    Task<UsuarioResponse> CreateAsync(CreateUsuarioRequest request);
    Task<UsuarioResponse> UpdateAsync(Guid id, UpdateUsuarioRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<UsuarioResponse?> GetByIdAsync(Guid id);
    Task<UsuarioResponse?> GetByMatriculaAsync(string matricula);
    Task<UsuarioResponse?> GetByEmailAsync(string email);
    Task<IEnumerable<UsuarioResponse>> GetAllAsync();
    Task<IEnumerable<UsuarioResponse>> GetActiveAsync();
    Task<IEnumerable<UsuarioResponse>> GetProfessoresAsync();
    Task<IEnumerable<UsuarioResponse>> GetAlunosAsync();
    Task<UsuarioResponse?> LoginAsync(LoginRequest request);
    Task<bool> MudarSenhaAsync(Guid id, MudarSenhaRequest request);
    Task<bool> ExistsByMatriculaAsync(string matricula);
    Task<bool> ExistsByEmailAsync(string email);
}