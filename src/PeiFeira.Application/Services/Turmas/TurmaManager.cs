using FluentValidation;
using PeiFeira.Application.Validators.Turma;
using PeiFeira.Communication.Requests.Turma;
using PeiFeira.Communication.Responses.Turmas;
using PeiFeira.Domain.Entities.Turmas;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Exception.ExeceptionsBases;

namespace PeiFeira.Application.Services.Turmas;

public class TurmaManager : ITurmaManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateTurmaRequestValidator _createValidator;
    private readonly UpdateTurmaRequestValidator _updateValidator;

    public TurmaManager(
        IUnitOfWork unitOfWork,
        CreateTurmaRequestValidator createValidator,
        UpdateTurmaRequestValidator updateValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<TurmaResponse> CreateAsync(CreateTurmaRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);

        // Validar se o semestre existe
        var semestre = await _unitOfWork.Semestres.GetByIdAsync(request.SemestreId);
        if (semestre == null)
        {
            throw new NotFoundException("Semestre", request.SemestreId);
        }

        // Validar se já existe turma com mesmo código
        if (await _unitOfWork.Turmas.ExistsByCodigoAsync(request.Codigo))
        {
            throw new ConflictException($"Já existe uma turma cadastrada com o código {request.Codigo}");
        }

        var turma = new Turma
        {
            SemestreId = request.SemestreId,
            Nome = request.Nome,
            Codigo = request.Codigo,
            Curso = request.Curso,
            Periodo = request.Periodo,
            Turno = request.Turno
        };

        await _unitOfWork.Turmas.CreateAsync(turma);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(turma);
    }

    public async Task<TurmaResponse> UpdateAsync(Guid id, UpdateTurmaRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);

        var turma = await _unitOfWork.Turmas.GetByIdAsync(id);
        if (turma == null)
        {
            throw new NotFoundException("Turma", id);
        }

        turma.Nome = request.Nome;
        turma.Curso = request.Curso;
        turma.Periodo = request.Periodo;
        turma.Turno = request.Turno;

        await _unitOfWork.Turmas.UpdateAsync(turma);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(turma);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var turma = await _unitOfWork.Turmas.GetByIdAsync(id);
        if (turma == null)
        {
            throw new NotFoundException("Turma", id);
        }

        var result = await _unitOfWork.Turmas.SoftDeleteAsync(id);
        if (result)
        {
            await _unitOfWork.SaveChangesAsync();
        }
        return result;
    }

    public async Task<TurmaResponse?> GetByIdAsync(Guid id)
    {
        var turma = await _unitOfWork.Turmas.GetByIdAsync(id);
        return turma != null ? MapToResponse(turma) : null;
    }

    public async Task<IEnumerable<TurmaResponse>> GetAllAsync()
    {
        var turmas = await _unitOfWork.Turmas.GetAllAsync();
        return turmas.Select(MapToResponse);
    }

    public async Task<IEnumerable<TurmaResponse>> GetActiveAsync()
    {
        var turmas = await _unitOfWork.Turmas.GetActiveAsync();
        return turmas.Select(MapToResponse);
    }

    public async Task<IEnumerable<TurmaResponse>> GetBySemestreIdAsync(Guid semestreId)
    {
        var turmas = await _unitOfWork.Turmas.GetBySemestreIdAsync(semestreId);
        return turmas.Select(MapToResponse);
    }

    public async Task<IEnumerable<TurmaResponse>> GetByCursoAsync(string curso)
    {
        var turmas = await _unitOfWork.Turmas.GetByCursoAsync(curso);
        return turmas.Select(MapToResponse);
    }

    public async Task<TurmaResponse?> GetByCodigoAsync(string codigo)
    {
        var turma = await _unitOfWork.Turmas.GetByCodigoAsync(codigo);
        return turma != null ? MapToResponse(turma) : null;
    }

    private static TurmaResponse MapToResponse(Turma turma)
    {
        return new TurmaResponse
        {
            Id = turma.Id,
            IsActive = turma.IsActive,
            SemestreId = turma.SemestreId,
            Nome = turma.Nome,
            Codigo = turma.Codigo,
            Curso = turma.Curso,
            Periodo = turma.Periodo,
            Turno = turma.Turno,
            CriadoEm = turma.CriadoEm,
            AlteradoEm = turma.AlteradoEm
        };
    }
}
