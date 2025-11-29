    using FluentValidation;
    using PeiFeira.Application.Validators.Projetos;
    using PeiFeira.Communication.Enums;
    using PeiFeira.Communication.Requests.Projetos;
    using PeiFeira.Communication.Responses.Projetos;
    using PeiFeira.Domain.Entities.Projetos;
    using PeiFeira.Domain.Enums;
    using PeiFeira.Domain.Interfaces.Repositories;
    using PeiFeira.Exception.ExeceptionsBases;

    namespace PeiFeira.Application.Services.Projetos;

    public class ProjetoManager : IProjetoManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateProjetoRequestValidator _createValidator;
        private readonly UpdateProjetoRequestValidator _updateValidator;

        public ProjetoManager(
            IUnitOfWork unitOfWork,
            CreateProjetoRequestValidator createValidator,
            UpdateProjetoRequestValidator updateValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ProjetoResponse> CreateAsync(CreateProjetoRequest request)
        {
            await _createValidator.ValidateAndThrowAsync(request);

            // Validar se a DisciplinaPI existe e está ativa
            var disciplinaPI = await _unitOfWork.DisciplinasPI.GetByIdAsync(request.DisciplinaPIId);
            if (disciplinaPI == null)
            {
                throw new NotFoundException("DisciplinaPI", request.DisciplinaPIId);
            }

            if (!disciplinaPI.IsActive)
            {
                throw new ConflictException("A DisciplinaPI não está ativa");
            }

            // Validar se a Equipe existe e está ativa
            var equipe = await _unitOfWork.Equipes.GetByIdAsync(request.EquipeId);
            if (equipe == null)
            {
                throw new NotFoundException("Equipe", request.EquipeId);
            }

            if (!equipe.IsActive)
            {
                throw new ConflictException("A equipe não está ativa");
            }

            // Validar se a equipe já tem um projeto
            var equipeJaTemProjeto = await _unitOfWork.Projetos.EquipeJaTemProjetoAsync(request.EquipeId);
            if (equipeJaTemProjeto)
            {
                throw new ConflictException("A equipe já possui um projeto cadastrado");
            }

            // VALIDAÇÃO CRÍTICA: Todos os membros da equipe devem estar matriculados em turmas da DisciplinaPI
            var membrosEquipe = await _unitOfWork.MembrosEquipe.GetMembrosComUsuarioAsync(request.EquipeId);
            var membrosAtivos = membrosEquipe.Where(m => m.IsActive).ToList();

            foreach (var membro in membrosAtivos)
            {
                var estaMatriculado = await _unitOfWork.AlunoTurmas.AlunoEstaEmAlgumaTurmaDaDisciplinaAsync(
                    membro.PerfilAlunoId, request.DisciplinaPIId);

                if (!estaMatriculado)
                {
                    var nomeMembro = membro.PerfilAluno?.Usuario?.Nome ?? "Membro não identificado";
                    throw new ConflictException(
                        $"O membro '{nomeMembro}' não está matriculado em nenhuma turma da disciplina PI. " +
                        $"Todos os membros da equipe devem estar matriculados.");
                }
            }

            var projeto = new Projeto
            {
                DisciplinaPIId = request.DisciplinaPIId,
                EquipeId = request.EquipeId,
                Titulo = request.Titulo,
                DesafioProposto = request.DesafioProposto,
                Status = StatusProjeto.EmAndamento,
                NomeEmpresa = request.NomeEmpresa,
                EnderecoCompleto = request.EnderecoCompleto,
                Cidade = request.Cidade,
                RedeSocial = request.RedeSocial,
                Contato = request.Contato,
                NomeResponsavel = request.NomeResponsavel,
                CargoResponsavel = request.CargoResponsavel,
                TelefoneResponsavel = request.TelefoneResponsavel,
                EmailResponsavel = request.EmailResponsavel,
                RedesSociaisResponsavel = request.RedesSociaisResponsavel
            };

            await _unitOfWork.Projetos.CreateAsync(projeto);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponse(projeto);
        }

        public async Task<ProjetoResponse> UpdateAsync(Guid id, UpdateProjetoRequest request)
        {
            await _updateValidator.ValidateAndThrowAsync(request);

            var projeto = await _unitOfWork.Projetos.GetByIdAsync(id);
            if (projeto == null)
            {
                throw new NotFoundException("Projeto", id);
            }

            projeto.Titulo = request.Titulo;
            projeto.DesafioProposto = request.DesafioProposto;
            projeto.Status = (StatusProjeto)request.Status;
            projeto.NomeEmpresa = request.NomeEmpresa;
            projeto.EnderecoCompleto = request.EnderecoCompleto;
            projeto.Cidade = request.Cidade;
            projeto.RedeSocial = request.RedeSocial;
            projeto.Contato = request.Contato;
            projeto.NomeResponsavel = request.NomeResponsavel;
            projeto.CargoResponsavel = request.CargoResponsavel;
            projeto.TelefoneResponsavel = request.TelefoneResponsavel;
            projeto.EmailResponsavel = request.EmailResponsavel;
            projeto.RedesSociaisResponsavel = request.RedesSociaisResponsavel;

            await _unitOfWork.Projetos.UpdateAsync(projeto);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponse(projeto);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var projeto = await _unitOfWork.Projetos.GetByIdAsync(id);
            if (projeto == null)
            {
                throw new NotFoundException("Projeto", id);
            }

            var result = await _unitOfWork.Projetos.SoftDeleteAsync(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<ProjetoResponse?> GetByIdAsync(Guid id)
        {
            var projeto = await _unitOfWork.Projetos.GetByIdAsync(id);
            return projeto != null ? MapToResponse(projeto) : null;
        }

        public async Task<ProjetoDetailResponse?> GetByIdWithDetailsAsync(Guid id)
        {
            var projeto = await _unitOfWork.Projetos.GetWithEquipeAsync(id);
            return projeto != null ? MapToDetailResponse(projeto) : null;
        }

        public async Task<IEnumerable<ProjetoResponse>> GetAllAsync()
        {
            var projetos = await _unitOfWork.Projetos.GetAllAsync();
            return projetos.Select(MapToResponse);
        }

        public async Task<IEnumerable<ProjetoResponse>> GetActiveAsync()
        {
            var projetos = await _unitOfWork.Projetos.GetActiveAsync();
            return projetos.Select(MapToResponse);
        }

        public async Task<ProjetoResponse?> GetByEquipeIdAsync(Guid equipeId)
        {
            var projeto = await _unitOfWork.Projetos.GetByEquipeIdAsync(equipeId);
            return projeto != null ? MapToResponse(projeto) : null;
        }

        public async Task<IEnumerable<ProjetoResponse>> GetByDisciplinaPIIdAsync(Guid disciplinaPIId)
        {
            var projetos = await _unitOfWork.Projetos.GetAllAsync();
            var projetosFiltrados = projetos.Where(p => p.DisciplinaPIId == disciplinaPIId);
            return projetosFiltrados.Select(MapToResponse);
        }

        public async Task<IEnumerable<ProjetoResponse>> GetProjetosComEmpresaAsync()
        {
            var projetos = await _unitOfWork.Projetos.GetProjetosComEmpresaAsync();
            return projetos.Select(MapToResponse);
        }

        public async Task<IEnumerable<ProjetoResponse>> GetProjetosAcademicosAsync()
        {
            var projetos = await _unitOfWork.Projetos.GetProjetosAcademicosAsync();
            return projetos.Select(MapToResponse);
        }

        private static ProjetoResponse MapToResponse(Projeto projeto)
        {
            return new ProjetoResponse
            {
                Id = projeto.Id,
                IsActive = projeto.IsActive,
                DisciplinaPIId = projeto.DisciplinaPIId,
                EquipeId = projeto.EquipeId,
                Titulo = projeto.Titulo,
                DesafioProposto = projeto.DesafioProposto,
                Status = (StatusProjetoDto)projeto.Status,
                NomeEmpresa = projeto.NomeEmpresa,
                EnderecoCompleto = projeto.EnderecoCompleto,
                Cidade = projeto.Cidade,
                RedeSocial = projeto.RedeSocial,
                Contato = projeto.Contato,
                NomeResponsavel = projeto.NomeResponsavel,
                CargoResponsavel = projeto.CargoResponsavel,
                TelefoneResponsavel = projeto.TelefoneResponsavel,
                EmailResponsavel = projeto.EmailResponsavel,
                RedesSociaisResponsavel = projeto.RedesSociaisResponsavel,
                NomeEquipe = projeto.Equipe?.Nome,
                NomeDisciplinaPI = projeto.DisciplinaPI?.Nome,
                QuantidadeMembros = projeto.Equipe?.Membros?.Count ?? 0,
                CriadoEm = projeto.CriadoEm,
                AlteradoEm = projeto.AlteradoEm
            };
        }

        private static ProjetoDetailResponse MapToDetailResponse(Projeto projeto)
        {
            return new ProjetoDetailResponse
            {
                Id = projeto.Id,
                IsActive = projeto.IsActive,
                DisciplinaPIId = projeto.DisciplinaPIId,
                EquipeId = projeto.EquipeId,
                Titulo = projeto.Titulo,
                DesafioProposto = projeto.DesafioProposto,
                Status = (StatusProjetoDto)projeto.Status,
                NomeEmpresa = projeto.NomeEmpresa,
                EnderecoCompleto = projeto.EnderecoCompleto,
                Cidade = projeto.Cidade,
                RedeSocial = projeto.RedeSocial,
                Contato = projeto.Contato,
                NomeResponsavel = projeto.NomeResponsavel,
                CargoResponsavel = projeto.CargoResponsavel,
                TelefoneResponsavel = projeto.TelefoneResponsavel,
                EmailResponsavel = projeto.EmailResponsavel,
                RedesSociaisResponsavel = projeto.RedesSociaisResponsavel,
                CriadoEm = projeto.CriadoEm,
                AlteradoEm = projeto.AlteradoEm,
                Equipe = projeto.Equipe != null ? new EquipeProjetoInfo
                {
                    Id = projeto.Equipe.Id,
                    Nome = projeto.Equipe.Nome,
                    Lider = projeto.Equipe.Lider != null ? new LiderProjetoInfo
                    {
                        Id = projeto.Equipe.Lider.Id,
                        Nome = projeto.Equipe.Lider.Usuario?.Nome ?? string.Empty,
                        Email = projeto.Equipe.Lider.Usuario?.Email ?? string.Empty
                    } : null,
                    Membros = projeto.Equipe.Membros?.Select(m => new MembroProjetoInfo
                    {
                        Id = m.Id,
                        Nome = m.PerfilAluno?.Usuario?.Nome ?? string.Empty,
                        Email = m.PerfilAluno?.Usuario?.Email ?? string.Empty
                    }).ToList() ?? new List<MembroProjetoInfo>()
                } : null,
                DisciplinaPI = projeto.DisciplinaPI != null ? new DisciplinaPIProjetoInfo
                {
                    Id = projeto.DisciplinaPI.Id,
                    Nome = projeto.DisciplinaPI.Nome,
                    Professor = projeto.DisciplinaPI.Professor?.Usuario?.Nome
                } : null,
                Avaliacoes = new List<AvaliacaoResumo>() // TODO: Implementar quando tiver AvaliacaoManager
            };
        }
    }
