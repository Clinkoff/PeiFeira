using PeiFeira.Domain.Entities.Avaliacoes;

namespace PeiFeira.Domain.Interfaces.Avaliacoes;

public interface IAvaliacaoRepository : IBaseRepository<Avaliacao>
{
    Task<IEnumerable<Avaliacao>> GetByEquipeIdAsync(Guid equipeId);
    Task<IEnumerable<Avaliacao>> GetByAvaliadorIdAsync(Guid avaliadorId); // As avaliações podem ter mais de um professor, o que analisa na feira e o que acompanha o projeto.

    Task<bool> JaAvaliadoAsync(Guid equipeId, Guid avaliadorId);
    Task<bool> PodeAvaliarAsync(Guid equipeId, Guid avaliadorId);  // Verifica se o avaliador pode avaliar a equipe (não é o professor que acompanhou o processo)

    Task<decimal> GetNotaAvaliacaoEquipeAsync(Guid equipeId); // Retorna a nota média das avaliações de uma equipe
    Task<decimal> GetMediaGeralAsync();

    Task<IEnumerable<Avaliacao>> GetAvaliacoesPorFaixaNotaAsync(decimal notaMin, decimal notaMax);
}
 