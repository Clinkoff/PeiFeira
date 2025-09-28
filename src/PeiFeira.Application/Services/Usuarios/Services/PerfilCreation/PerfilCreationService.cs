using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Application.Services.Usuarios.Services.PerfilCreation;

public class PerfilCreationService
{
    private readonly IEnumerable<IPerfilCreationStrategy> _strategies;

    public PerfilCreationService(IEnumerable<IPerfilCreationStrategy> strategies)
    {
        _strategies = strategies;
    }

    public async Task CreatePerfilAsync(Usuario usuario, CreateUsuarioRequest request)
    {
        var strategy = _strategies.FirstOrDefault(s => s.CanHandle(usuario.Role));

        if (strategy == null)
            throw new NotSupportedException($"Tipo de usuário {usuario.Role} não suportado");

        await strategy.CreatePerfilAsync(usuario, request);
    }
}