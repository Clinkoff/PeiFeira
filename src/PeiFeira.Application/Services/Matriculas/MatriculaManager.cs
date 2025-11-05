using FluentValidation;
using PeiFeira.Application.Services.Matriculas.Services;
using PeiFeira.Application.Validators.Matriculas;
using PeiFeira.Communication.Requests.Matriculas;
using PeiFeira.Communication.Responses.Matriculas;
using PeiFeira.Domain.Entities.Turmas;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Exception.ExeceptionsBases;

namespace PeiFeira.Application.Services.Matriculas;

public class MatriculaManager : IMatriculaManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateMatriculaRequestValidator _createValidator;
    private readonly IMatriculaTransactionService _transactionService;

    public MatriculaManager(
        IUnitOfWork unitOfWork,
        CreateMatriculaRequestValidator createValidator,
        IMatriculaTransactionService transactionService)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _transactionService = transactionService;
    }

    public async Task<MatriculaResponse> MatricularAlunoAsync(CreateMatriculaRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);
        await new MatriculaBusinessValidator(_unitOfWork).ValidateAndThrowAsync(request);

        var perfis = await _unitOfWork.PerfisAluno.GetAllAsync();
        var perfilAluno = perfis.FirstOrDefault(p => p.UsuarioId == request.UsuarioId);

        if (perfilAluno == null)
            throw new NotFoundException("PerfilAluno", request.UsuarioId);

        var matriculaId = await _transactionService.CreateMatriculaAsync(
            request.TurmaId,
            perfilAluno.Id
        );

        var matriculaCompleta = await _unitOfWork.AlunoTurmas.GetByIdAsync(matriculaId);
        return MapToResponse(matriculaCompleta!);
    }

    public async Task<bool> TransferirAlunoAsync(Guid perfilAlunoId, Guid novaTurmaId)
    {
        // Validar se nova turma existe
        var turma = await _unitOfWork.Turmas.GetByIdAsync(novaTurmaId);
        if (turma == null)
        {
            throw new NotFoundException("Turma", novaTurmaId);
        }

        // Validar se aluno existe
        var aluno = await _unitOfWork.PerfisAluno.GetByIdAsync(perfilAlunoId);
        if (aluno == null)
        {
            throw new NotFoundException("PerfilAluno", perfilAlunoId);
        }

        // Criar nova matrícula (desativa a antiga automaticamente)
        await MatricularAlunoAsync(new CreateMatriculaRequest
        {
            TurmaId = novaTurmaId,
            UsuarioId = perfilAlunoId
        });

        return true;
    }

    public async Task<bool> DesmatricularAlunoAsync(Guid matriculaId)
    {
        var matricula = await _unitOfWork.AlunoTurmas.GetByIdAsync(matriculaId);
        if (matricula == null)
        {
            throw new NotFoundException("Matrícula", matriculaId);
        }

        matricula.IsAtual = false;
        await _unitOfWork.AlunoTurmas.UpdateAsync(matricula);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<MatriculaResponse?> GetByIdAsync(Guid id)
    {
        var matricula = await _unitOfWork.AlunoTurmas.GetByIdAsync(id);
        return matricula != null ? MapToResponse(matricula) : null;
    }

    public async Task<IEnumerable<MatriculaResponse>> GetByTurmaIdAsync(Guid turmaId)
    {
        var matriculas = await _unitOfWork.AlunoTurmas.GetByTurmaIdAsync(turmaId);
        return matriculas.Select(MapToResponse);
    }

    public async Task<IEnumerable<MatriculaResponse>> GetByAlunoIdAsync(Guid perfilAlunoId)
    {
        var matriculas = await _unitOfWork.AlunoTurmas.GetByPerfilAlunoIdAsync(perfilAlunoId);
        return matriculas.Select(MapToResponse);
    }

    public async Task<MatriculaResponse?> GetMatriculaAtualByAlunoAsync(Guid perfilAlunoId)
    {
        var matricula = await _unitOfWork.AlunoTurmas.GetMatriculaAtualByPerfilAlunoIdAsync(perfilAlunoId);
        return matricula != null ? MapToResponse(matricula) : null;
    }

    private static MatriculaResponse MapToResponse(AlunoTurma matricula)
    {
        return new MatriculaResponse
        {
            Id = matricula.Id,
            IsActive = matricula.IsActive,
            TurmaId = matricula.TurmaId,
            PerfilAlunoId = matricula.PerfilAlunoId,
            DataMatricula = matricula.DataMatricula,
            IsAtual = matricula.IsAtual,
            NomeTurma = matricula.Turma?.Nome,
            CodigoTurma = matricula.Turma?.Codigo,
            NomeAluno = matricula.PerfilAluno?.Usuario?.Nome,
            MatriculaAluno = matricula.PerfilAluno?.Usuario?.Matricula,
            CriadoEm = matricula.CriadoEm,
            AlteradoEm = matricula.AlteradoEm
        };
    }
}