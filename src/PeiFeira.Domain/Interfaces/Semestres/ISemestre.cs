using PeiFeira.Domain.Entities.Semestres;

namespace PeiFeira.Domain.Interfaces.Semestres;

public interface ISemestre : IBaseRepository<Semestre>
{
    Task<Semestre?> GetByAnoAndPeriodoAsync(int ano, int periodo);
    Task<IEnumerable<Semestre>> GetByAnoAsync(int ano);
    Task<bool> ExistsByAnoAndPeriodoAsync(int ano, int periodo);
}
