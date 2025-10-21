using FluentValidation;
using PeiFeira.Application.Validators.Semestre;
using PeiFeira.Communication.Requests.Semestres;
using PeiFeira.Communication.Responses.Semestres;
using PeiFeira.Domain.Entities.Semestres;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Exception.ExeceptionsBases;

namespace PeiFeira.Application.Services.Semestres;

public class SemestreManager : ISemestreManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateSemestreRequestValidator _createValidator;
    private readonly UpdateSemestreRequestValidator _updateValidator;

    public SemestreManager(
        IUnitOfWork unitOfWork,
        CreateSemestreRequestValidator createValidator,
        UpdateSemestreRequestValidator updateValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<SemestreResponse> CreateAsync(CreateSemestreRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);

        // Validar se já existe semestre com mesmo ano e período
        if (await _unitOfWork.Semestres.ExistsByAnoAndPeriodoAsync(request.Ano, request.Periodo))
        {
            throw new ConflictException($"Já existe um semestre cadastrado para {request.Ano}.{request.Periodo}");
        }

        var semestre = new Semestre
        {
            Nome = request.Nome,
            Ano = request.Ano,
            Periodo = request.Periodo,
            DataInicio = request.DataInicio,
            DataFim = request.DataFim
        };

        await _unitOfWork.Semestres.CreateAsync(semestre);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(semestre);
    }

    public async Task<SemestreResponse> UpdateAsync(Guid id, UpdateSemestreRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);

        var semestre = await _unitOfWork.Semestres.GetByIdAsync(id);
        if (semestre == null)
        {
            throw new NotFoundException("Semestre", id);
        }

        semestre.Nome = request.Nome;
        semestre.DataInicio = request.DataInicio;
        semestre.DataFim = request.DataFim;

        await _unitOfWork.Semestres.UpdateAsync(semestre);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(semestre);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var semestre = await _unitOfWork.Semestres.GetByIdAsync(id);
        if (semestre == null)
        {
            throw new NotFoundException("Semestre", id);
        }

        var result = await _unitOfWork.Semestres.SoftDeleteAsync(id);
        if (result)
        {
            await _unitOfWork.SaveChangesAsync();
        }
        return result;
    }

    public async Task<SemestreResponse?> GetByIdAsync(Guid id)
    {
        var semestre = await _unitOfWork.Semestres.GetByIdAsync(id);
        return semestre != null ? MapToResponse(semestre) : null;
    }

    public async Task<IEnumerable<SemestreResponse>> GetAllAsync()
    {
        var semestres = await _unitOfWork.Semestres.GetAllAsync();
        return semestres.Select(MapToResponse);
    }

    public async Task<IEnumerable<SemestreResponse>> GetActiveAsync()
    {
        var semestres = await _unitOfWork.Semestres.GetActiveAsync();
        return semestres.Select(MapToResponse);
    }

    public async Task<IEnumerable<SemestreResponse>> GetByAnoAsync(int ano)
    {
        var semestres = await _unitOfWork.Semestres.GetByAnoAsync(ano);
        return semestres.Select(MapToResponse);
    }

    public async Task<SemestreResponse?> GetByAnoAndPeriodoAsync(int ano, int periodo)
    {
        var semestre = await _unitOfWork.Semestres.GetByAnoAndPeriodoAsync(ano, periodo);
        return semestre != null ? MapToResponse(semestre) : null;
    }

    private static SemestreResponse MapToResponse(Semestre semestre)
    {
        return new SemestreResponse
        {
            Id = semestre.Id,
            IsActive = semestre.IsActive,
            Nome = semestre.Nome,
            Ano = semestre.Ano,
            Periodo = semestre.Periodo,
            DataInicio = semestre.DataInicio,
            DataFim = semestre.DataFim,
            CriadoEm = semestre.CriadoEm,
            AlteradoEm = semestre.AlteradoEm
        };
    }
}