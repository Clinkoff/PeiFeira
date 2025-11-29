// PeiFeira.Application/Services/Dashboard/DashboardManager.cs
using PeiFeira.Communication.Responses.Dashboard;
using PeiFeira.Domain.Enums;
using PeiFeira.Domain.Interfaces.Repositories;

namespace PeiFeira.Application.Services.Dashboard;

public class DashboardManager : IDashboardManager
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardStatsResponse> GetStatsAsync()
    {
        var totalSemestres = await _unitOfWork.Semestres.CountAsync();
        var usuarios = await _unitOfWork.Usuarios.GetAllAsync();
        var totalTurmas = await _unitOfWork.Turmas.CountAsync();
        var disciplinasPI = await _unitOfWork.DisciplinasPI.GetAllAsync();
        var totalEquipes = await _unitOfWork.Equipes.CountAsync();
        var projetos = await _unitOfWork.Projetos.GetAllAsync();

        var totalAlunos = usuarios.Count(u => u.Role == UserRole.Aluno);
        var totalProfessores = usuarios.Count(u => u.Role == UserRole.Professor);
        var disciplinasPIAtivas = disciplinasPI.Count(d => d.Status == StatusProjetoIntegrador.Ativo);
        var projetosEmAndamento = projetos.Count(p => p.Status == StatusProjeto.EmAndamento);
        var equipesAtivas = await _unitOfWork.Equipes.CountAsync(e => e.IsActive);

        return new DashboardStatsResponse
        {
            TotalSemestres = totalSemestres,
            TotalUsuarios = usuarios.Count(),
            TotalAlunos = totalAlunos,
            TotalProfessores = totalProfessores,
            TotalTurmas = totalTurmas,
            TotalDisciplinasPI = disciplinasPI.Count(),
            TotalEquipes = totalEquipes,
            TotalProjetos = projetos.Count(),
            DisciplinasPIAtivas = disciplinasPIAtivas,
            ProjetosEmAndamento = projetosEmAndamento,
            EquipesAtivas = equipesAtivas
        };
    }

    public async Task<IEnumerable<ProjetosPorStatusResponse>> GetProjetosPorStatusAsync()
    {
        var projetos = await _unitOfWork.Projetos.GetAllAsync();

        var grouped = projetos
            .GroupBy(p => p.Status)
            .Select(g => new ProjetosPorStatusResponse
            {
                Status = GetStatusLabel(g.Key),
                Quantidade = g.Count(),
                Cor = GetStatusColor(g.Key)
            })
            .OrderByDescending(x => x.Quantidade)
            .ToList();

        return grouped;
    }

    public async Task<IEnumerable<DisciplinasPorSemestreResponse>> GetDisciplinasPorSemestreAsync()
    {
        var disciplinas = await _unitOfWork.DisciplinasPI.GetAllAsync();
        var semestres = await _unitOfWork.Semestres.GetAllAsync();

        var grouped = disciplinas
            .GroupBy(d => d.SemestreId)
            .Select(g =>
            {
                var semestre = semestres.FirstOrDefault(s => s.Id == g.Key);
                return new DisciplinasPorSemestreResponse
                {
                    Semestre = semestre?.Nome ?? "Sem semestre",
                    Quantidade = g.Count()
                };
            })
            .OrderBy(x => x.Semestre)
            .ToList();

        return grouped;
    }

    public async Task<IEnumerable<ProjetosPorMesResponse>> GetProjetosPorMesAsync()
    {
        var projetos = await _unitOfWork.Projetos.GetAllAsync();
        var hoje = DateTime.UtcNow;
        var seisUltimosMeses = Enumerable.Range(0, 6)
            .Select(i => hoje.AddMonths(-i))
            .OrderBy(d => d)
            .ToList();

        var resultado = seisUltimosMeses.Select(mes =>
        {
            var criados = projetos.Count(p =>
                p.CriadoEm.Year == mes.Year && p.CriadoEm.Month == mes.Month);

            var concluidos = projetos.Count(p =>
                p.Status == StatusProjeto.Concluido &&
                p.AlteradoEm.HasValue &&
                p.AlteradoEm.Value.Year == mes.Year &&
                p.AlteradoEm.Value.Month == mes.Month);

            return new ProjetosPorMesResponse
            {
                Mes = mes.ToString("MMM/yy", new System.Globalization.CultureInfo("pt-BR")),
                Criados = criados,
                Concluidos = concluidos
            };
        }).ToList();

        return resultado;
    }

    public async Task<IEnumerable<AlunosPorTurmaResponse>> GetAlunosPorTurmaAsync()
    {
        var turmas = await _unitOfWork.Turmas.GetAllAsync();
        var alunosTurma = await _unitOfWork.AlunoTurmas.GetAllAsync();

        var grouped = alunosTurma
            .Where(at => at.IsAtual) // Apenas alunos atuais
            .GroupBy(at => at.TurmaId)
            .Select(g =>
            {
                var turma = turmas.FirstOrDefault(t => t.Id == g.Key);
                return new AlunosPorTurmaResponse
                {
                    Turma = turma?.Nome ?? "Sem turma",
                    Quantidade = g.Count()
                };
            })
            .OrderByDescending(x => x.Quantidade)
            .Take(10) // Top 10
            .ToList();

        return grouped;
    }

    public async Task<IEnumerable<AtividadeRecenteResponse>> GetAtividadesRecentesAsync()
    {
        var atividades = new List<AtividadeRecenteResponse>();

        // Projetos recentes (últimos 10)
        var projetos = await _unitOfWork.Projetos.GetAllAsync();
        var projetosRecentes = projetos
            .OrderByDescending(p => p.CriadoEm)
            .Take(10)
            .Select(p => new AtividadeRecenteResponse
            {
                Id = p.Id,
                Tipo = "projeto",
                Titulo = "Novo projeto criado",
                Descricao = p.Titulo,
                Data = p.CriadoEm,
                Icone = "📝"
            });

        atividades.AddRange(projetosRecentes);

        // Equipes recentes (últimas 10)
        var equipes = await _unitOfWork.Equipes.GetAllAsync();
        var equipesRecentes = equipes
            .OrderByDescending(e => e.CriadoEm)
            .Take(10)
            .Select(e => new AtividadeRecenteResponse
            {
                Id = e.Id,
                Tipo = "equipe",
                Titulo = "Nova equipe criada",
                Descricao = e.Nome,
                Data = e.CriadoEm,
                Icone = "👥"
            });

        atividades.AddRange(equipesRecentes);

        // Disciplinas PI recentes (últimas 10)
        var disciplinas = await _unitOfWork.DisciplinasPI.GetAllAsync();
        var disciplinasRecentes = disciplinas
            .OrderByDescending(d => d.CriadoEm)
            .Take(10)
            .Select(d => new AtividadeRecenteResponse
            {
                Id = d.Id,
                Tipo = "disciplina",
                Titulo = "Nova disciplina PI criada",
                Descricao = d.Nome,
                Data = d.CriadoEm,
                Icone = "📚"
            });

        atividades.AddRange(disciplinasRecentes);

        // Usuários recentes (últimos 10)
        var usuarios = await _unitOfWork.Usuarios.GetAllAsync();
        var usuariosRecentes = usuarios
            .OrderByDescending(u => u.CriadoEm)
            .Take(10)
            .Select(u => new AtividadeRecenteResponse
            {
                Id = u.Id,
                Tipo = "usuario",
                Titulo = "Novo usuário cadastrado",
                Descricao = $"{u.Nome} - {GetRoleLabel(u.Role)}",
                Data = u.CriadoEm,
                Icone = "👤"
            });

        atividades.AddRange(usuariosRecentes);

        // Ordenar todas por data e pegar as 20 mais recentes
        return atividades
            .OrderByDescending(a => a.Data)
            .Take(20)
            .ToList();
    }

    private static string GetStatusLabel(StatusProjeto status)
    {
        return status switch
        {
            StatusProjeto.EmAndamento => "Em Andamento",
            StatusProjeto.Concluido => "Concluído",
            StatusProjeto.Cancelado => "Cancelado",
            StatusProjeto.Aprovado => "Aprovado",
            StatusProjeto.Reprovado => "Reprovado",
            _ => "Desconhecido"
        };
    }

    private static string GetStatusColor(StatusProjeto status)
    {
        return status switch
        {
            StatusProjeto.EmAndamento => "#6F73D2",
            StatusProjeto.Concluido => "#4ADE80",
            StatusProjeto.Cancelado => "#EF4444",
            StatusProjeto.Aprovado => "#10B981",
            StatusProjeto.Reprovado => "#F87171",
            _ => "#6B7280"
        };
    }

    private static string GetRoleLabel(UserRole role)
    {
        return role switch
        {
            UserRole.Admin => "Administrador",
            UserRole.Coordenador => "Coordenador",
            UserRole.Professor => "Professor",
            UserRole.Aluno => "Aluno",
            _ => "Desconhecido"
        };
    }
}