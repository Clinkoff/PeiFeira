using PeiFeira.Domain.Entities.Equipes;

namespace PeiFeira.Domain.Interfaces.Equipes;

public interface IEquipeRepository : IBaseRepository<Equipe>
{
    Task<Equipe?> GetByCodigoConviteAsync(string codigoConvite);
    Task<Equipe?> GetByLiderIdAsync(Guid liderId);
    Task<IEnumerable<Equipe>> GetByEquipesByUsuariosIdAsync(Guid usuarioId);

    Task<Equipe?> GetWithProjetoAsync(Guid id); // Equipe + lista de membros
    Task<Equipe?> GetWithMembrosAsync(Guid id); // Equipe + projeto
    Task<Equipe?> GetCompleteAsync(Guid id);
    Task<IEnumerable<Equipe>> GetComProjetoAsync();

    Task<string> GenerateCodigoConviteAsync(); // Gera um código de convite único
    Task<bool> IsCodigoConviteValidoAsync(string codigo); // Verifica se o código de convite é válido (existe e está ativo)
}
