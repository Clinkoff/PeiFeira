using PeiFeira.Domain.Entities.Projetos;

namespace PeiFeira.Domain.Interfaces;

public interface IProjetoRepository : IBaseRepository<Projeto>
{
    Task<Projeto?> GetByEquipeIdAsync(Guid equipeId);
    Task<bool> EquipeJaTemProjetoAsync(Guid equipeId); // Verifica se a equipe já tem um projeto, talvez seja descecessário.

    Task<IEnumerable<Projeto>> GetByTemaAsync(string tema);
    Task<IEnumerable<Projeto>> SearchByTituloAsync(string titulo);

    Task<IEnumerable<Projeto>> GetProjetosComEmpresaAsync();
    Task<IEnumerable<Projeto>> GetProjetosAcademicosAsync();
    Task<IEnumerable<Projeto>> GetByEmpresaAsync(string nomeEmpresa);

    Task<Projeto?> GetWithEquipeAsync(Guid id); // Projeto + dados da equipe 

}
