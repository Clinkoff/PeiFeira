using PeiFeira.Communication.Requests.Avaliacoes;
using PeiFeira.Communication.Responses.Avaliacoes;

namespace PeiFeira.Application.Services.Avaliacoes;

public interface IAvaliacaoManager
{
    Task<AvaliacaoResponse> CreateAsync(CreateAvaliacaoRequest request);
    Task<AvaliacaoResponse> UpdateAsync(Guid id, UpdateAvaliacaoRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<AvaliacaoResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<AvaliacaoResponse>> GetAllAsync();
    Task<IEnumerable<AvaliacaoResponse>> GetByEquipeIdAsync(Guid equipeId);
    Task<IEnumerable<AvaliacaoResponse>> GetByAvaliadorIdAsync(Guid avaliadorId);
    Task<decimal> GetMediaEquipeAsync(Guid equipeId);
    Task<decimal> GetMediaGeralAsync();
    Task<IEnumerable<AvaliacaoResponse>> GetAvaliacoesPorFaixaNotaAsync(decimal notaMin, decimal notaMax);
}
