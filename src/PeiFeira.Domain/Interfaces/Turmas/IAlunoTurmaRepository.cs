using PeiFeira.Domain.Entities.Turmas;

namespace PeiFeira.Domain.Interfaces.Turmas;

public interface IAlunoTurmaRepository : IBaseRepository<AlunoTurma>
{
    Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(Guid turmaId);
    Task<IEnumerable<AlunoTurma>> GetByPerfilAlunoIdAsync(Guid perfilAlunoId);
    Task<AlunoTurma?> GetMatriculaAtualByPerfilAlunoIdAsync(Guid perfilAlunoId);
    Task<bool> ExistsMatriculaAtivaAsync(Guid perfilAlunoId, Guid turmaId);
    Task<bool> AlunoEstaEmAlgumaTurmaDaDisciplinaAsync(Guid perfilAlunoId, Guid disciplinaPIId);
}