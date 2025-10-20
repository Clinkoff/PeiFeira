using FluentValidation;
using PeiFeira.Application.Validators.DisciplinaPI;
using PeiFeira.Communication.Enums;
using PeiFeira.Communication.Requests.DisciplinaPI;
using PeiFeira.Communication.Responses.DisciplinaPI;
using PeiFeira.Domain.Entities.DisciplinasPI;
using PeiFeira.Domain.Enums;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Exception.ExeceptionsBases;

namespace PeiFeira.Application.Services.DisciplinasPI;

public class DisciplinaPIManager : IDisciplinaPIManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateDisciplinaPIRequestValidator _createValidator;
    private readonly UpdateDisciplinaPIRequestValidator _updateValidator;

    public DisciplinaPIManager(
        IUnitOfWork unitOfWork,
        CreateDisciplinaPIRequestValidator createValidator,
        UpdateDisciplinaPIRequestValidator updateValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<DisciplinaPIResponse> CreateAsync(CreateDisciplinaPIRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);

        // Validar se já existe disciplina com mesmo nome no semestre
        if (await _unitOfWork.DisciplinasPI.ExistsByNomeAndSemestreIdAsync(request.Nome, request.SemestreId))
            throw new ConflictException("Já existe uma disciplina com este nome neste semestre");

        // Criar disciplina
        var disciplina = new DisciplinaPI
        {
            SemestreId = request.SemestreId,
            PerfilProfessorId = request.PerfilProfessorId,
            Nome = request.Nome,
            TemaGeral = request.TemaGeral,
            Descricao = request.Descricao,
            Objetivos = request.Objetivos,
            DataInicio = request.DataInicio,
            DataFim = request.DataFim,
            Status = StatusProjetoIntegrador.Ativo
        };

        var created = await _unitOfWork.DisciplinasPI.CreateAsync(disciplina);

        // Associar turmas
        foreach (var turmaId in request.TurmaIds)
        {
            var disciplinaTurma = new DisciplinaPITurma
            {
                DisciplinaPIId = created.Id,
                TurmaId = turmaId
            };
            await _unitOfWork.DisciplinaPITurmas.CreateAsync(disciplinaTurma);
        }

        await _unitOfWork.SaveChangesAsync();

        // Recarregar com includes
        var disciplinaCompleta = await _unitOfWork.DisciplinasPI.GetByIdWithIncludesAsync(created.Id);
        return MapToResponse(disciplinaCompleta!);
    }

    public async Task<DisciplinaPIResponse> UpdateAsync(Guid id, UpdateDisciplinaPIRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);

        var disciplina = await _unitOfWork.DisciplinasPI.GetByIdAsync(id);
        if (disciplina == null)
            throw new NotFoundException("Disciplina PI não encontrada");

        // Atualizar dados
        disciplina.Nome = request.Nome;
        disciplina.TemaGeral = request.TemaGeral;
        disciplina.Descricao = request.Descricao;
        disciplina.Objetivos = request.Objetivos;
        disciplina.DataInicio = request.DataInicio;
        disciplina.DataFim = request.DataFim;
        disciplina.Status = (StatusProjetoIntegrador)request.Status;

        await _unitOfWork.DisciplinasPI.UpdateAsync(disciplina);

        // Atualizar turmas associadas
        var turmasAtuais = await _unitOfWork.DisciplinaPITurmas.GetByDisciplinaPIIdAsync(id);
        var turmasAtuaisIds = turmasAtuais.Select(t => t.TurmaId).ToList();

        // Remover turmas que não estão mais na lista
        foreach (var turmaAtual in turmasAtuais)
        {
            if (!request.TurmaIds.Contains(turmaAtual.TurmaId))
            {
                await _unitOfWork.DisciplinaPITurmas.DeleteAsync(turmaAtual.Id);
            }
        }

        // Adicionar novas turmas
        foreach (var turmaId in request.TurmaIds)
        {
            if (!turmasAtuaisIds.Contains(turmaId))
            {
                var disciplinaTurma = new DisciplinaPITurma
                {
                    DisciplinaPIId = id,
                    TurmaId = turmaId
                };
                await _unitOfWork.DisciplinaPITurmas.CreateAsync(disciplinaTurma);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        // Recarregar com includes
        var disciplinaCompleta = await _unitOfWork.DisciplinasPI.GetByIdWithIncludesAsync(id);
        return MapToResponse(disciplinaCompleta!);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _unitOfWork.DisciplinasPI.SoftDeleteAsync(id);
        if (result)
            await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<DisciplinaPIResponse?> GetByIdAsync(Guid id)
    {
        var disciplina = await _unitOfWork.DisciplinasPI.GetByIdWithIncludesAsync(id);
        return disciplina != null ? MapToResponse(disciplina) : null;
    }

    public async Task<DisciplinaPIDetailResponse?> GetByIdWithDetailsAsync(Guid id)
    {
        var disciplina = await _unitOfWork.DisciplinasPI.GetByIdWithIncludesAsync(id);
        return disciplina != null ? MapToDetailResponse(disciplina) : null;
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> GetAllAsync()
    {
        var disciplinas = await _unitOfWork.DisciplinasPI.GetAllAsync();
        var disciplinasCompletas = new List<DisciplinaPI>();

        foreach (var disciplina in disciplinas)
        {
            var completa = await _unitOfWork.DisciplinasPI.GetByIdWithIncludesAsync(disciplina.Id);
            if (completa != null)
                disciplinasCompletas.Add(completa);
        }

        return disciplinasCompletas.Select(MapToResponse);
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> GetActiveAsync()
    {
        var disciplinas = await _unitOfWork.DisciplinasPI.GetAtivasAsync();
        return disciplinas.Select(MapToResponse);
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> GetBySemestreIdAsync(Guid semestreId)
    {
        var disciplinas = await _unitOfWork.DisciplinasPI.GetBySemestreIdAsync(semestreId);
        return disciplinas.Select(MapToResponse);
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> GetByProfessorIdAsync(Guid perfilProfessorId)
    {
        var disciplinas = await _unitOfWork.DisciplinasPI.GetByProfessorIdAsync(perfilProfessorId);
        return disciplinas.Select(MapToResponse);
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> GetByTurmaIdAsync(Guid turmaId)
    {
        var disciplinas = await _unitOfWork.DisciplinasPI.GetAtivasByTurmaIdAsync(turmaId);
        return disciplinas.Select(MapToResponse);
    }

    public async Task<bool> ExistsByNomeAndSemestreAsync(string nome, Guid semestreId)
    {
        return await _unitOfWork.DisciplinasPI.ExistsByNomeAndSemestreIdAsync(nome, semestreId);
    }

    public async Task<bool> AssociarTurmaAsync(Guid disciplinaPIId, Guid turmaId)
    {
        if (await _unitOfWork.DisciplinaPITurmas.ExistsByDisciplinaPIIdAndTurmaIdAsync(disciplinaPIId, turmaId))
            return false;

        var disciplinaTurma = new DisciplinaPITurma
        {
            DisciplinaPIId = disciplinaPIId,
            TurmaId = turmaId
        };

        await _unitOfWork.DisciplinaPITurmas.CreateAsync(disciplinaTurma);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoverTurmaAsync(Guid disciplinaPIId, Guid turmaId)
    {
        var result = await _unitOfWork.DisciplinaPITurmas.DeleteByDisciplinaPIIdAndTurmaIdAsync(disciplinaPIId, turmaId);
        if (result)
            await _unitOfWork.SaveChangesAsync();
        return result;
    }

    private static DisciplinaPIResponse MapToResponse(DisciplinaPI disciplina)
    {
        return new DisciplinaPIResponse
        {
            Id = disciplina.Id,
            IsActive = disciplina.IsActive,
            SemestreId = disciplina.SemestreId,
            PerfilProfessorId = disciplina.PerfilProfessorId,
            Nome = disciplina.Nome,
            TemaGeral = disciplina.TemaGeral,
            Descricao = disciplina.Descricao,
            Objetivos = disciplina.Objetivos,
            DataInicio = disciplina.DataInicio,
            DataFim = disciplina.DataFim,
            Status = (StatusProjetoIntegradorDto)disciplina.Status,
            CriadoEm = disciplina.CriadoEm,
            AlteradoEm = disciplina.AlteradoEm,
            NomeSemestre = disciplina.Semestre?.Nome,
            NomeProfessor = disciplina.Professor?.Usuario?.Nome,
            QuantidadeTurmas = disciplina.DisciplinaPITurmas?.Count ?? 0,
            QuantidadeProjetos = disciplina.Projetos?.Count ?? 0
        };
    }

    private static DisciplinaPIDetailResponse MapToDetailResponse(DisciplinaPI disciplina)
    {
        return new DisciplinaPIDetailResponse
        {
            Id = disciplina.Id,
            IsActive = disciplina.IsActive,
            SemestreId = disciplina.SemestreId,
            PerfilProfessorId = disciplina.PerfilProfessorId,
            Nome = disciplina.Nome,
            TemaGeral = disciplina.TemaGeral,
            Descricao = disciplina.Descricao,
            Objetivos = disciplina.Objetivos,
            DataInicio = disciplina.DataInicio,
            DataFim = disciplina.DataFim,
            Status = (StatusProjetoIntegradorDto)disciplina.Status,
            CriadoEm = disciplina.CriadoEm,
            AlteradoEm = disciplina.AlteradoEm,
            Semestre = disciplina.Semestre != null ? new SemestreInfo
            {
                Id = disciplina.Semestre.Id,
                Nome = disciplina.Semestre.Nome,
                Ano = disciplina.Semestre.Ano,
                Periodo = disciplina.Semestre.Periodo
            } : null,
            Professor = disciplina.Professor?.Usuario != null ? new ProfessorInfo
            {
                Id = disciplina.Professor.Id,
                Nome = disciplina.Professor.Usuario.Nome,
                Email = disciplina.Professor.Usuario.Email,
                Departamento = disciplina.Professor.Departamento
            } : null,
            Turmas = disciplina.DisciplinaPITurmas?.Select(dt => new TurmaInfo
            {
                Id = dt.Turma.Id,
                Nome = dt.Turma.Nome,
                Curso = dt.Turma.Curso
            }).ToList() ?? new List<TurmaInfo>(),
            Projetos = disciplina.Projetos?.Select(p => new ProjetoInfo
            {
                Id = p.Id,
                Titulo = p.Titulo,
                NomeEquipe = p.Equipe?.Nome
            }).ToList() ?? new List<ProjetoInfo>()
        };
    }
}
