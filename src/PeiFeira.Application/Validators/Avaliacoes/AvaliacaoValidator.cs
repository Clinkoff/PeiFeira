using FluentValidation;
using PeiFeira.Domain.Entities.Avaliacoes;

namespace PeiFeira.Application.Validators.Avaliacoes;

public class AvaliacaoValidator : BaseValidator<Avaliacao>
{
    public AvaliacaoValidator()
    {
        RuleFor(a => a.EquipeId)
            .NotEmpty()
            .WithMessage("Equipe é obrigatória")
            .Must(BeValidGuid)
            .WithMessage("ID da equipe deve ser válido");

        RuleFor(a => a.AvaliadorId)
            .NotEmpty()
            .WithMessage("Avaliador é obrigatório")
            .Must(BeValidGuid)
            .WithMessage("ID do avaliador deve ser válido");

        ValidateScore(a => a.RelevanciaProblema, "Relevância do Problema");
        ValidateScore(a => a.FundamentacaoProblema, "Fundamentação do Problema");
        ValidateScore(a => a.FocoSolucao, "Foco da Solução");
        ValidateScore(a => a.ViabilidadeSolucao, "Viabilidade da Solução");
        ValidateScore(a => a.ClarezaApresentacao, "Clareza da Apresentação");
        ValidateScore(a => a.DominioAssunto, "Domínio do Assunto");
        ValidateScore(a => a.TransmissaoInformacoes, "Transmissão de Informações");
        ValidateScore(a => a.PadronizacaoApresentacao, "Padronização da Apresentação");
        ValidateScore(a => a.LinguagemTempo, "Linguagem e Tempo");
        ValidateScore(a => a.QualidadeRespostas, "Qualidade das Respostas");
    }

    private void ValidateScore(System.Linq.Expressions.Expression<System.Func<Avaliacao, int>> expression, string fieldName)
    {
        RuleFor(expression)
            .InclusiveBetween(0, 5)
            .WithMessage($"{fieldName} deve ser entre 0 e 5");
    }
}