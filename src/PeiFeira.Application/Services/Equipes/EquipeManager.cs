using FluentValidation;
using PeiFeira.Application.Validators.Equipes;
using PeiFeira.Communication.Requests.Equipes;
using PeiFeira.Communication.Responses.Equipes;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Exception.ExeceptionsBases;

namespace PeiFeira.Application.Services.Equipes;

public class EquipeManager : IEquipeManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateEquipeRequestValidator _createValidator;
    private readonly UpdateEquipeRequestValidator _updateValidator;

    public EquipeManager(
        IUnitOfWork unitOfWork,
        CreateEquipeRequestValidator createValidator,
        UpdateEquipeRequestValidator updateValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<EquipeResponse> CreateAsync(CreateEquipeRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);

        // Validar se o líder existe e é PerfilAluno
        var lider = await _unitOfWork.PerfisAluno.GetByIdAsync(request.LiderPerfilAlunoId);
        if (lider == null)
        {
            throw new NotFoundException("PerfilAluno", request.LiderPerfilAlunoId);
        }

        // Validar se o líder já é líder de outra equipe ativa
        var equipeExistente = await _unitOfWork.Equipes.GetByLiderIdAsync(request.LiderPerfilAlunoId);
        if (equipeExistente != null && equipeExistente.IsActive)
        {
            throw new ConflictException($"O aluno já é líder de uma equipe ativa: {equipeExistente.Nome}");
        }

        // Gerar código de convite único
        var codigoConvite = await _unitOfWork.Equipes.GenerateCodigoConviteAsync();

        var equipe = new Equipe
        {
            LiderPerfilAlunoId = request.LiderPerfilAlunoId,
            Nome = request.Nome,
            CodigoConvite = codigoConvite
        };

        await _unitOfWork.Equipes.CreateAsync(equipe);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(equipe);
    }

    public async Task<EquipeResponse> UpdateAsync(Guid id, UpdateEquipeRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);

        var equipe = await _unitOfWork.Equipes.GetByIdAsync(id);
        if (equipe == null)
        {
            throw new NotFoundException("Equipe", id);
        }

        equipe.Nome = request.Nome;

        await _unitOfWork.Equipes.UpdateAsync(equipe);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(equipe);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var equipe = await _unitOfWork.Equipes.GetByIdAsync(id);
        if (equipe == null)
        {
            throw new NotFoundException("Equipe", id);
        }

        var result = await _unitOfWork.Equipes.SoftDeleteAsync(id);
        if (result)
        {
            await _unitOfWork.SaveChangesAsync();
        }
        return result;
    }

    public async Task<EquipeResponse?> GetByIdAsync(Guid id)
    {
        var equipe = await _unitOfWork.Equipes.GetByIdAsync(id);
        return equipe != null ? MapToResponse(equipe) : null;
    }

    public async Task<EquipeDetailResponse?> GetByIdWithDetailsAsync(Guid id)
    {
        var equipe = await _unitOfWork.Equipes.GetCompleteAsync(id);
        return equipe != null ? MapToDetailResponse(equipe) : null;
    }

    public async Task<IEnumerable<EquipeResponse>> GetAllAsync()
    {
        var equipes = await _unitOfWork.Equipes.GetAllAsync();
        return equipes.Select(MapToResponse);
    }

    public async Task<IEnumerable<EquipeResponse>> GetActiveAsync()
    {
        var equipes = await _unitOfWork.Equipes.GetActiveAsync();
        return equipes.Select(MapToResponse);
    }

    public async Task<EquipeResponse?> GetByLiderIdAsync(Guid liderId)
    {
        var equipe = await _unitOfWork.Equipes.GetByLiderIdAsync(liderId);
        return equipe != null ? MapToResponse(equipe) : null;
    }

    public async Task<EquipeResponse?> GetByCodigoConviteAsync(string codigo)
    {
        var equipe = await _unitOfWork.Equipes.GetByCodigoConviteAsync(codigo);
        return equipe != null ? MapToResponse(equipe) : null;
    }

    public async Task<EquipeResponse> RegenerarCodigoConviteAsync(Guid id)
    {
        var equipe = await _unitOfWork.Equipes.GetByIdAsync(id);
        if (equipe == null)
        {
            throw new NotFoundException("Equipe", id);
        }

        // Gerar novo código de convite único
        var novoCodigoConvite = await _unitOfWork.Equipes.GenerateCodigoConviteAsync();
        equipe.CodigoConvite = novoCodigoConvite;

        await _unitOfWork.Equipes.UpdateAsync(equipe);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(equipe);
    }

    private static EquipeResponse MapToResponse(Equipe equipe)
    {
        return new EquipeResponse
        {
            Id = equipe.Id,
            IsActive = equipe.IsActive,
            LiderPerfilAlunoId = equipe.LiderPerfilAlunoId,
            Nome = equipe.Nome,
            CodigoConvite = equipe.CodigoConvite,
            UrlQrCode = equipe.UrlQrCode,
            NomeLider = equipe.Lider?.Usuario?.Nome,
            QuantidadeMembros = equipe.Membros?.Count ?? 0,
            TemProjeto = equipe.Projeto != null,
            CriadoEm = equipe.CriadoEm,
            AlteradoEm = equipe.AlteradoEm
        };
    }

    private static EquipeDetailResponse MapToDetailResponse(Equipe equipe)
    {
        return new EquipeDetailResponse
        {
            Id = equipe.Id,
            IsActive = equipe.IsActive,
            LiderPerfilAlunoId = equipe.LiderPerfilAlunoId,
            Nome = equipe.Nome,
            CodigoConvite = equipe.CodigoConvite,
            UrlQrCode = equipe.UrlQrCode,
            QuantidadeMembros = equipe.Membros?.Count ?? 0,
            TemProjeto = equipe.Projeto != null,
            CriadoEm = equipe.CriadoEm,
            AlteradoEm = equipe.AlteradoEm,
            Lider = equipe.Lider != null ? new LiderInfo
            {
                Id = equipe.Lider.Id,
                Nome = equipe.Lider.Usuario?.Nome ?? string.Empty,
                Email = equipe.Lider.Usuario?.Email ?? string.Empty
            } : null,
            Membros = equipe.Membros?.Select(m => new MembroInfo
            {
                Nome = m.PerfilAluno?.Usuario?.Nome ?? string.Empty,
                Email = m.PerfilAluno?.Usuario?.Email ?? string.Empty,
                DataEntrada = m.CriadoEm
            }).ToList() ?? new List<MembroInfo>(),
            Projeto = equipe.Projeto != null ? new ProjetoInfo
            {
                Id = equipe.Projeto.Id,
                Titulo = equipe.Projeto.Titulo ?? string.Empty,
                Status = equipe.Projeto.ToString()
            } : null
        };
    }
}
