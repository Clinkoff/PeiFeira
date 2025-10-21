using FluentValidation;
using PeiFeira.Application.Validators.MembroEquipe;
using PeiFeira.Communication.Requests.MembroEquipe;
using PeiFeira.Communication.Responses.MembroEquipe;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Exception.ExeceptionsBases;

namespace PeiFeira.Application.Services.MembrosEquipes;

public class MembroEquipeManager : IMembroEquipeManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AddMembroEquipeRequestValidator _addValidator;

    public MembroEquipeManager(
        IUnitOfWork unitOfWork,
        AddMembroEquipeRequestValidator addValidator)
    {
        _unitOfWork = unitOfWork;
        _addValidator = addValidator;
    }

    public async Task<MembroEquipeResponse> AddMembroAsync(AddMembroEquipeRequest request)
    {
        await _addValidator.ValidateAndThrowAsync(request);

        // Validar se a equipe existe e está ativa
        var equipe = await _unitOfWork.Equipes.GetByIdAsync(request.EquipeId);
        if (equipe == null)
        {
            throw new NotFoundException("Equipe", request.EquipeId);
        }

        if (!equipe.IsActive)
        {
            throw new ConflictException("A equipe não está ativa");
        }

        // Validar se o PerfilAluno existe
        var perfilAluno = await _unitOfWork.PerfisAluno.GetByIdAsync(request.PerfilAlunoId);
        if (perfilAluno == null)
        {
            throw new NotFoundException("PerfilAluno", request.PerfilAlunoId);
        }

        // Validar se aluno já está em outra equipe ativa
        var jaEstaEmEquipe = await _unitOfWork.MembrosEquipe.UsuarioJaEstaEmEquipeAsync(request.PerfilAlunoId);
        if (jaEstaEmEquipe)
        {
            throw new ConflictException("O aluno já está participando de uma equipe ativa");
        }

        // Validar se aluno já é membro desta equipe
        var jaEMembroDestaEquipe = await _unitOfWork.MembrosEquipe.IsUsuarioInEquipeAsync(request.EquipeId, request.PerfilAlunoId);
        if (jaEMembroDestaEquipe)
        {
            throw new ConflictException("O aluno já é membro desta equipe");
        }

        var membroEquipe = new Domain.Entities.Equipes.MembroEquipe
        {
            EquipeId = request.EquipeId,
            PerfilAlunoId = request.PerfilAlunoId,
            IngressouEm = DateTime.UtcNow,
            Cargo = TeamMemberRole.Membro
        };

        await _unitOfWork.MembrosEquipe.CreateAsync(membroEquipe);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(membroEquipe);
    }

    public async Task<bool> RemoveMembroAsync(Guid equipeId, Guid perfilAlunoId)
    {
        // Buscar o membro
        var membro = await _unitOfWork.MembrosEquipe.GetByEquipeAndUsuarioAsync(equipeId, perfilAlunoId);
        if (membro == null)
        {
            throw new NotFoundException("MembroEquipe não encontrado");
        }

        // Validar se o membro é o líder da equipe
        var lider = await _unitOfWork.MembrosEquipe.GetLiderEquipeAsync(equipeId);
        if (lider != null && lider.PerfilAlunoId == perfilAlunoId)
        {
            throw new ConflictException("O líder não pode ser removido da equipe");
        }

        // Soft delete
        var result = await _unitOfWork.MembrosEquipe.SoftDeleteAsync(membro.Id);
        if (result)
        {
            await _unitOfWork.SaveChangesAsync();
        }
        return result;
    }

    public async Task<IEnumerable<MembroEquipeResponse>> GetByEquipeIdAsync(Guid equipeId)
    {
        var membros = await _unitOfWork.MembrosEquipe.GetMembrosComUsuarioAsync(equipeId);
        return membros.Select(MapToResponse);
    }

    public async Task<IEnumerable<MembroEquipeResponse>> GetByPerfilAlunoIdAsync(Guid perfilAlunoId)
    {
        var membros = await _unitOfWork.MembrosEquipe.GetByUsuarioIdAsync(perfilAlunoId);
        return membros.Select(MapToResponse);
    }

    public async Task<MembroEquipeResponse?> GetByIdAsync(Guid id)
    {
        var membro = await _unitOfWork.MembrosEquipe.GetByIdAsync(id);
        return membro != null ? MapToResponse(membro) : null;
    }

    public async Task<bool> IsMembroAtivo(Guid equipeId, Guid perfilAlunoId)
    {
        return await _unitOfWork.MembrosEquipe.IsUsuarioInEquipeAsync(equipeId, perfilAlunoId);
    }

    private static MembroEquipeResponse MapToResponse(MembroEquipe membro) => new MembroEquipeResponse
    {
        Id = membro.Id,
        IsActive = membro.IsActive,
        EquipeId = membro.EquipeId,
        PerfilAlunoId = membro.PerfilAlunoId,
        DataEntrada = membro.IngressouEm,
        NomeEquipe = membro.Equipe?.Nome,
        NomeMembro = membro.PerfilAluno?.Usuario?.Nome,
        EmailMembro = membro.PerfilAluno?.Usuario?.Email,
        CriadoEm = membro.CriadoEm,
        AlteradoEm = membro.AlteradoEm
    };
}
