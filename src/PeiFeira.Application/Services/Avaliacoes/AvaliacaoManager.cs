using FluentValidation;
using PeiFeira.Application.Validators.Avaliacoes;
using PeiFeira.Communication.Requests.Avaliacoes;
using PeiFeira.Communication.Responses.Avaliacoes;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Exception.ExeceptionsBases;

namespace PeiFeira.Application.Services.Avaliacoes;

public class AvaliacaoManager : IAvaliacaoManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateAvaliacaoRequestValidator _createValidator;
    private readonly UpdateAvaliacaoRequestValidator _updateValidator;

    public AvaliacaoManager(
        IUnitOfWork unitOfWork,
        CreateAvaliacaoRequestValidator createValidator,
        UpdateAvaliacaoRequestValidator updateValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<AvaliacaoResponse> CreateAsync(CreateAvaliacaoRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);

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

        // Validar se o Avaliador (PerfilProfessor) existe
        var avaliador = await _unitOfWork.PerfisProfessor.GetByIdAsync(request.AvaliadorId);
        if (avaliador == null)
        {
            throw new NotFoundException("PerfilProfessor", request.AvaliadorId);
        }

        // Verificar se pode avaliar (regras de negócio do repositório)
        var podeAvaliar = await _unitOfWork.Avaliacoes.PodeAvaliarAsync(request.EquipeId, request.AvaliadorId);
        if (!podeAvaliar)
        {
            throw new ConflictException("O professor não pode avaliar esta equipe");
        }

        // Calcular pontuação total e nota final
        var pontuacaoTotal = CalcularPontuacaoTotal(
            request.RelevanciaProblema,
            request.FundamentacaoProblema,
            request.FocoSolucao,
            request.ViabilidadeSolucao,
            request.ClarezaApresentacao,
            request.DominioAssunto,
            request.TransmissaoInformacoes,
            request.PadronizacaoApresentacao,
            request.LinguagemTempo,
            request.QualidadeRespostas
        );

        var notaFinal = CalcularNotaFinal(pontuacaoTotal);

        var avaliacao = new Avaliacao
        {
            EquipeId = request.EquipeId,
            AvaliadorId = request.AvaliadorId,
            RelevanciaProblema = request.RelevanciaProblema,
            FundamentacaoProblema = request.FundamentacaoProblema,
            FocoSolucao = request.FocoSolucao,
            ViabilidadeSolucao = request.ViabilidadeSolucao,
            ClarezaApresentacao = request.ClarezaApresentacao,
            DominioAssunto = request.DominioAssunto,
            TransmissaoInformacoes = request.TransmissaoInformacoes,
            PadronizacaoApresentacao = request.PadronizacaoApresentacao,
            LinguagemTempo = request.LinguagemTempo,
            QualidadeRespostas = request.QualidadeRespostas,
            PontuacaoTotal = pontuacaoTotal,
            NotaFinal = notaFinal,
            Comentarios = request.Comentarios
        };

        await _unitOfWork.Avaliacoes.CreateAsync(avaliacao);
        await _unitOfWork.SaveChangesAsync();

        // Buscar novamente com includes para obter dados relacionados
        var avaliacaoCriada = await _unitOfWork.Avaliacoes.GetByIdAsync(avaliacao.Id);
        return MapToResponse(avaliacaoCriada!);
    }

    public async Task<AvaliacaoResponse> UpdateAsync(Guid id, UpdateAvaliacaoRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);

        var avaliacao = await _unitOfWork.Avaliacoes.GetByIdAsync(id);
        if (avaliacao == null)
        {
            throw new NotFoundException("Avaliacao", id);
        }

        // Recalcular pontuação total e nota final
        var pontuacaoTotal = CalcularPontuacaoTotal(
            request.RelevanciaProblema,
            request.FundamentacaoProblema,
            request.FocoSolucao,
            request.ViabilidadeSolucao,
            request.ClarezaApresentacao,
            request.DominioAssunto,
            request.TransmissaoInformacoes,
            request.PadronizacaoApresentacao,
            request.LinguagemTempo,
            request.QualidadeRespostas
        );

        var notaFinal = CalcularNotaFinal(pontuacaoTotal);

        avaliacao.RelevanciaProblema = request.RelevanciaProblema;
        avaliacao.FundamentacaoProblema = request.FundamentacaoProblema;
        avaliacao.FocoSolucao = request.FocoSolucao;
        avaliacao.ViabilidadeSolucao = request.ViabilidadeSolucao;
        avaliacao.ClarezaApresentacao = request.ClarezaApresentacao;
        avaliacao.DominioAssunto = request.DominioAssunto;
        avaliacao.TransmissaoInformacoes = request.TransmissaoInformacoes;
        avaliacao.PadronizacaoApresentacao = request.PadronizacaoApresentacao;
        avaliacao.LinguagemTempo = request.LinguagemTempo;
        avaliacao.QualidadeRespostas = request.QualidadeRespostas;
        avaliacao.PontuacaoTotal = pontuacaoTotal;
        avaliacao.NotaFinal = notaFinal;
        avaliacao.Comentarios = request.Comentarios;

        await _unitOfWork.Avaliacoes.UpdateAsync(avaliacao);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(avaliacao);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var avaliacao = await _unitOfWork.Avaliacoes.GetByIdAsync(id);
        if (avaliacao == null)
        {
            throw new NotFoundException("Avaliacao", id);
        }

        var result = await _unitOfWork.Avaliacoes.SoftDeleteAsync(id);
        if (result)
        {
            await _unitOfWork.SaveChangesAsync();
        }
        return result;
    }

    public async Task<AvaliacaoResponse?> GetByIdAsync(Guid id)
    {
        var avaliacao = await _unitOfWork.Avaliacoes.GetByIdAsync(id);
        return avaliacao != null ? MapToResponse(avaliacao) : null;
    }

    public async Task<IEnumerable<AvaliacaoResponse>> GetAllAsync()
    {
        var avaliacoes = await _unitOfWork.Avaliacoes.GetAllAsync();
        return avaliacoes.Select(MapToResponse);
    }

    public async Task<IEnumerable<AvaliacaoResponse>> GetByEquipeIdAsync(Guid equipeId)
    {
        var avaliacoes = await _unitOfWork.Avaliacoes.GetByEquipeIdAsync(equipeId);
        return avaliacoes.Select(MapToResponse);
    }

    public async Task<IEnumerable<AvaliacaoResponse>> GetByAvaliadorIdAsync(Guid avaliadorId)
    {
        var avaliacoes = await _unitOfWork.Avaliacoes.GetByAvaliadorIdAsync(avaliadorId);
        return avaliacoes.Select(MapToResponse);
    }

    public async Task<decimal> GetMediaEquipeAsync(Guid equipeId)
    {
        return await _unitOfWork.Avaliacoes.GetNotaAvaliacaoEquipeAsync(equipeId);
    }

    public async Task<decimal> GetMediaGeralAsync()
    {
        return await _unitOfWork.Avaliacoes.GetMediaGeralAsync();
    }

    public async Task<IEnumerable<AvaliacaoResponse>> GetAvaliacoesPorFaixaNotaAsync(decimal notaMin, decimal notaMax)
    {
        if (notaMin < 0 || notaMax > 10 || notaMin > notaMax)
        {
            throw new ArgumentException("Faixa de nota inválida. Min deve ser >= 0, Max <= 10 e Min <= Max");
        }

        var avaliacoes = await _unitOfWork.Avaliacoes.GetAvaliacoesPorFaixaNotaAsync(notaMin, notaMax);
        return avaliacoes.Select(MapToResponse);
    }

    private static decimal CalcularPontuacaoTotal(
        int relevanciaProblema,
        int fundamentacaoProblema,
        int focoSolucao,
        int viabilidadeSolucao,
        int clarezaApresentacao,
        int dominioAssunto,
        int transmissaoInformacoes,
        int padronizacaoApresentacao,
        int linguagemTempo,
        int qualidadeRespostas)
    {
        return relevanciaProblema +
               fundamentacaoProblema +
               focoSolucao +
               viabilidadeSolucao +
               clarezaApresentacao +
               dominioAssunto +
               transmissaoInformacoes +
               padronizacaoApresentacao +
               linguagemTempo +
               qualidadeRespostas;
    }

    private static decimal CalcularNotaFinal(decimal pontuacaoTotal)
    {
        // Total possível: 10 critérios * 5 pontos = 50 pontos
        // Converter para nota 0-10
        const decimal pontuacaoMaxima = 50m;
        return Math.Round((pontuacaoTotal / pontuacaoMaxima) * 10, 2);
    }

    private static AvaliacaoResponse MapToResponse(Avaliacao avaliacao)
    {
        return new AvaliacaoResponse
        {
            Id = avaliacao.Id,
            IsActive = avaliacao.IsActive,
            EquipeId = avaliacao.EquipeId,
            AvaliadorId = avaliacao.AvaliadorId,
            RelevanciaProblema = avaliacao.RelevanciaProblema,
            FundamentacaoProblema = avaliacao.FundamentacaoProblema,
            FocoSolucao = avaliacao.FocoSolucao,
            ViabilidadeSolucao = avaliacao.ViabilidadeSolucao,
            ClarezaApresentacao = avaliacao.ClarezaApresentacao,
            DominioAssunto = avaliacao.DominioAssunto,
            TransmissaoInformacoes = avaliacao.TransmissaoInformacoes,
            PadronizacaoApresentacao = avaliacao.PadronizacaoApresentacao,
            LinguagemTempo = avaliacao.LinguagemTempo,
            QualidadeRespostas = avaliacao.QualidadeRespostas,
            PontuacaoTotal = avaliacao.PontuacaoTotal,
            NotaFinal = avaliacao.NotaFinal,
            Comentarios = avaliacao.Comentarios,
            NomeEquipe = avaliacao.Equipe?.Nome,
            NomeAvaliador = avaliacao.Avaliador?.Usuario?.Nome,
            CriadoEm = avaliacao.CriadoEm,
            AlteradoEm = avaliacao.AlteradoEm
        };
    }
}
