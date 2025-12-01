using PeiFeira.Communication.Requests.ConviteEquipe;
using PeiFeira.Communication.Responses.ConviteEquipe;

namespace PeiFeira.Application.Services.ConviteEquipe;

public interface IConviteEquipeManager
{
    Task<ConviteEquipeResponse> EnviarConviteAsync(CreateConviteEquipeRequest request);
    Task<ConviteEquipeResponse> AceitarConviteAsync(Guid conviteId, Guid perfilAlunoId);
    Task<ConviteEquipeResponse> RecusarConviteAsync(Guid conviteId, Guid perfilAlunoId);
    Task<ConviteEquipeResponse> CancelarConviteAsync(Guid conviteId, Guid perfilAlunoId);
    Task<ConviteEquipeResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<ConviteEquipeResponse>> GetConvitesPendentesAsync(Guid perfilAlunoId);
    Task<IEnumerable<ConviteEquipeResponse>> GetConvitesByEquipeAsync(Guid equipeId);
    Task<int> ContarConvitesPendentesAsync(Guid perfilAlunoId);
}
