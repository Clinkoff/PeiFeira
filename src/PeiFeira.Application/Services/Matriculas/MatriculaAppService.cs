using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.Matriculas;
using PeiFeira.Communication.Responses.Matriculas;

namespace PeiFeira.Application.Services.Matriculas;

public class MatriculaAppService
{
    IMatriculaManager _matriculaManager;
    ILogger<MatriculaAppService> _logger;

    public MatriculaAppService(
        IMatriculaManager matriculaManager,
        ILogger<MatriculaAppService> logger)
    {
        _matriculaManager = matriculaManager;
        _logger = logger;
    }

    public Task<MatriculaResponse> MatricularAlunoAsync(CreateMatriculaRequest request)
    {
        _logger.LogInformation("Iniciando matrícula do aluno. PerfilAlunoId: {PerfilAlunoId}, TurmaId: {TurmaId}", request.PerfilAlunoId, request.TurmaId);
        return _matriculaManager.MatricularAlunoAsync(request);
    }

    public Task<bool> DesmatricularAlunoAsync(Guid matriculaId)
    {
        _logger.LogInformation("Iniciando desmatrícula. MatriculaId: {MatriculaId}", matriculaId);
        return _matriculaManager.DesmatricularAlunoAsync(matriculaId);
    }

    public Task<bool> TransferirAlunoAsync(Guid perfilAlunoId, Guid novaTurmaId)
    {
        _logger.LogInformation("Buscando matrícula atual do aluno. PerfilAlunoId: {PerfilAlunoId}", perfilAlunoId);
        return _matriculaManager.TransferirAlunoAsync(perfilAlunoId, novaTurmaId);
    }

    public Task<MatriculaResponse?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando matrícula por ID: {Id}", id);
        return _matriculaManager.GetByIdAsync(id);
    }

    public Task<IEnumerable<MatriculaResponse>> GetByTurmaIdAsync(Guid turmaId){
        _logger.LogInformation("Buscando matrículas por TurmaId: {TurmaId}", turmaId);
        return _matriculaManager.GetByTurmaIdAsync(turmaId);
    }

    public Task<IEnumerable<MatriculaResponse>> GetByAlunoIdAsync(Guid perfilAlunoId){
        _logger.LogInformation("Buscando matrículas por PerfilAlunoId: {PerfilAlunoId}", perfilAlunoId);
        return _matriculaManager.GetByAlunoIdAsync(perfilAlunoId);
    }

    public Task<MatriculaResponse?> GetMatriculaAtualByAlunoAsync(Guid perfilAlunoId)
    {
        _logger.LogInformation("Buscando matrícula atual do aluno. PerfilAlunoId: {PerfilAlunoId}", perfilAlunoId);
        return _matriculaManager.GetMatriculaAtualByAlunoAsync(perfilAlunoId);
    }

}
