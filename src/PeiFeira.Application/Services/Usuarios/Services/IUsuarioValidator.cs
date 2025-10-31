using PeiFeira.Communication.Requests.Auth;
using PeiFeira.Communication.Requests.Usuario;

namespace PeiFeira.Application.Services.Usuarios.Services;

public interface IUsuarioValidator
{
    Task ValidateCreateRequestAsync(CreateUsuarioRequest request);
    Task ValidateUpdateRequestAsync(UpdateUsuarioRequest request);
    Task ValidateLoginRequestAsync(LoginRequest request);
    Task ValidateMudarSenhaRequestAsync(MudarSenhaRequest request);
    Task ValidateUniquenessAsync(string matricula, string email);
}