using PeiFeira.Communication.Requests.Matriculas;
using PeiFeira.Communication.Responses.Matriculas;

namespace PeiFeira.Application.Services.Matriculas;

public interface IMatriculaManager
{
    Task<MatriculaResponse> MatricularAlunoAsync(CreateMatriculaRequest request);
    Task<bool> TransferirAlunoAsync(Guid perfilAlunoId, Guid novaTurmaId);
    Task<bool> DesmatricularAlunoAsync(Guid matriculaId);
    Task<MatriculaResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<MatriculaResponse>> GetByTurmaIdAsync(Guid turmaId);
    Task<IEnumerable<MatriculaResponse>> GetByAlunoIdAsync(Guid perfilAlunoId);
    Task<MatriculaResponse?> GetMatriculaAtualByAlunoAsync(Guid perfilAlunoId);
}