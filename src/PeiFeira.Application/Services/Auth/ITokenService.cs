using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Application.Services.Auth;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
    int GetExpirationInSeconds();
}
