using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Application.Services.Usuarios.Services.PerfilCreation;

public interface IPerfilCreationStrategy
{
    bool CanHandle(UserRole role);
    Task CreatePerfilAsync(Usuario usuario, CreateUsuarioRequest request);
}