    using FluentValidation;
    using PeiFeira.Application.Validators.ConviteEquipe;
    using PeiFeira.Communication.Requests.ConviteEquipe;
    using PeiFeira.Communication.Responses.ConviteEquipe;
    using PeiFeira.Domain.Enums;
    using PeiFeira.Domain.Interfaces.Repositories;
    using PeiFeira.Exception.ExeceptionsBases;

    namespace PeiFeira.Application.Services.ConviteEquipe;

    public class ConviteEquipeManager : IConviteEquipeManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateConviteEquipeRequestValidator _createValidator;

        public ConviteEquipeManager(
            IUnitOfWork unitOfWork,
            CreateConviteEquipeRequestValidator createValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
        }

        public async Task<ConviteEquipeResponse> EnviarConviteAsync(CreateConviteEquipeRequest request)
        {
            await _createValidator.ValidateAndThrowAsync(request);

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

            // Validar se ConvidadoPor é o líder da equipe
            if (equipe.LiderPerfilAlunoId != request.ConvidadoPorId)
            {
                throw new ConflictException("Apenas o líder da equipe pode enviar convites");
            }

            // Validar se o convidado existe
            var convidado = await _unitOfWork.PerfisAluno.GetByIdAsync(request.ConvidadoId);
            if (convidado == null)
            {
                throw new NotFoundException("PerfilAluno convidado", request.ConvidadoId);
            }

            // Validar se o convidado já está em uma equipe ativa
            var jaEstaEmEquipe = await _unitOfWork.MembrosEquipe.UsuarioJaEstaEmEquipeAsync(request.ConvidadoId);
            if (jaEstaEmEquipe)
            {
                throw new ConflictException("O aluno convidado já está participando de uma equipe ativa");
            }

            // Validar se já existe convite pendente para este aluno nesta equipe
            var temConvitePendente = await _unitOfWork.ConvitesEquipe.TemConvitePendenteAsync(request.EquipeId, request.ConvidadoId);
            if (temConvitePendente)
            {
                throw new ConflictException("Já existe um convite pendente para este aluno nesta equipe");
            }

            var convite = new Domain.Entities.Equipes.ConviteEquipe
            {
                EquipeId = request.EquipeId,
                ConvidadoPorPerfilAlunoId = request.ConvidadoPorId,
                ConvidadoPerfilAlunoId = request.ConvidadoId,
                Mensagem = request.Mensagem,
                Status = StatusConvite.Pendente
            };

            await _unitOfWork.ConvitesEquipe.CreateAsync(convite);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponse(convite);
        }

        public async Task<ConviteEquipeResponse> AceitarConviteAsync(Guid conviteId, Guid perfilAlunoId)
        {
            var convite = await _unitOfWork.ConvitesEquipe.GetByIdAsync(conviteId);
            if (convite == null)
            {
                throw new NotFoundException("Convite", conviteId);
            }

            // Validar se quem está aceitando é o convidado
            if (convite.ConvidadoPerfilAlunoId != perfilAlunoId)
            {
                throw new ConflictException("Apenas o aluno convidado pode aceitar este convite");
            }

            // Validar se o convite está pendente
            if (convite.Status != StatusConvite.Pendente)
            {
                throw new ConflictException($"Este convite já foi {convite.Status.ToString().ToLower()}");
            }

            // Validar se o aluno já está em outra equipe ativa
            var jaEstaEmEquipe = await _unitOfWork.MembrosEquipe.UsuarioJaEstaEmEquipeAsync(perfilAlunoId);
            if (jaEstaEmEquipe)
            {
                throw new ConflictException("Você já está participando de uma equipe ativa");
            }

            // Usar transação para garantir atomicidade
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Atualizar status do convite
                convite.Status = StatusConvite.Aceito;
                convite.RespondidoEm = DateTime.UtcNow;
                await _unitOfWork.ConvitesEquipe.UpdateAsync(convite);

                // Criar MembroEquipe
                var membroEquipe = new Domain.Entities.Equipes.MembroEquipe
                {
                    EquipeId = convite.EquipeId,
                    PerfilAlunoId = perfilAlunoId,
                    IngressouEm = DateTime.UtcNow,
                    Cargo = TeamMemberRole.Membro
                };
                await _unitOfWork.MembrosEquipe.CreateAsync(membroEquipe);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return MapToResponse(convite);
        }

        public async Task<ConviteEquipeResponse> RecusarConviteAsync(Guid conviteId, Guid perfilAlunoId)
        {
            var convite = await _unitOfWork.ConvitesEquipe.GetByIdAsync(conviteId);
            if (convite == null)
            {
                throw new NotFoundException("Convite", conviteId);
            }

            // Validar se quem está recusando é o convidado
            if (convite.ConvidadoPerfilAlunoId != perfilAlunoId)
            {
                throw new ConflictException("Apenas o aluno convidado pode recusar este convite");
            }

            // Validar se o convite está pendente
            if (convite.Status != StatusConvite.Pendente)
            {
                throw new ConflictException($"Este convite já foi {convite.Status.ToString().ToLower()}");
            }

            // Atualizar status do convite
            convite.Status = StatusConvite.Rejeitado;
            convite.RespondidoEm = DateTime.UtcNow;

            await _unitOfWork.ConvitesEquipe.UpdateAsync(convite);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponse(convite);
        }

        public async Task<ConviteEquipeResponse> CancelarConviteAsync(Guid conviteId, Guid perfilAlunoId)
        {
            var convite = await _unitOfWork.ConvitesEquipe.GetByIdAsync(conviteId);
            if (convite == null)
            {
                throw new NotFoundException("Convite", conviteId);
            }

            // Validar se quem está cancelando é quem enviou o convite
            if (convite.ConvidadoPorPerfilAlunoId != perfilAlunoId)
            {
                throw new ConflictException("Apenas quem enviou o convite pode cancelá-lo");
            }

            // Validar se o convite está pendente
            if (convite.Status != StatusConvite.Pendente)
            {
                throw new ConflictException($"Este convite já foi {convite.Status.ToString().ToLower()}");
            }

            // Atualizar status do convite
            convite.Status = StatusConvite.Cancelado;
            convite.RespondidoEm = DateTime.UtcNow;

            await _unitOfWork.ConvitesEquipe.UpdateAsync(convite);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponse(convite);
        }

        public async Task<ConviteEquipeResponse?> GetByIdAsync(Guid id)
        {
            var convite = await _unitOfWork.ConvitesEquipe.GetByIdAsync(id);
            return convite != null ? MapToResponse(convite) : null;
        }

        public async Task<IEnumerable<ConviteEquipeResponse>> GetConvitesPendentesAsync(Guid perfilAlunoId)
        {
            var convites = await _unitOfWork.ConvitesEquipe.GetPendentesAsync(perfilAlunoId);
            return convites.Select(MapToResponse);
        }

        public async Task<IEnumerable<ConviteEquipeResponse>> GetConvitesByEquipeAsync(Guid equipeId)
        {
            var convites = await _unitOfWork.ConvitesEquipe.GetByEquipeIdAsync(equipeId);
            return convites.Select(MapToResponse);
        }

        private static ConviteEquipeResponse MapToResponse(Domain.Entities.Equipes.ConviteEquipe convite)
        {
            return new ConviteEquipeResponse
            {
                Id = convite.Id,
                IsActive = convite.IsActive,
                EquipeId = convite.EquipeId,
                ConvidadoPorId = convite.ConvidadoPorPerfilAlunoId,
                ConvidadoId = convite.ConvidadoPerfilAlunoId,
                Status = (Communication.Enums.StatusConvite)convite.Status,
                DataResposta = convite.RespondidoEm,
                Mensagem = convite.Mensagem,
                MotivoResposta = convite.MotivoResposta,
                NomeEquipe = convite.Equipe?.Nome,
                NomeConvidadoPor = convite.ConvidadoPor?.Usuario?.Nome,
                NomeConvidado = convite.Convidado?.Usuario?.Nome,
                CriadoEm = convite.CriadoEm,
                AlteradoEm = convite.AlteradoEm
            };
        }
    }
