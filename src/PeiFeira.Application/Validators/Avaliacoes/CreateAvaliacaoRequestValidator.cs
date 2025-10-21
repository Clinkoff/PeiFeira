using FluentValidation;
using PeiFeira.Communication.Requests.Avaliacoes;

namespace PeiFeira.Application.Validators.Avaliacoes;

public class CreateAvaliacaoRequestValidator : BaseValidator<CreateAvaliacaoRequest>
{
    public CreateAvaliacaoRequestValidator()
    {
        RuleFor(a => a.EquipeId)
            .NotEmpty().WithMessage("EquipeId é obrigatório")
            .Must(BeValidGuid).WithMessage("EquipeId inválido");

        RuleFor(a => a.AvaliadorId)
            .NotEmpty().WithMessage("AvaliadorId é obrigatório")
            .Must(BeValidGuid).WithMessage("AvaliadorId inválido");

        // Definição do problema (0-5)
        RuleFor(a => a.RelevanciaProblema)
            .InclusiveBetween(0, 5).WithMessage("Relevância do problema deve estar entre 0 e 5");

        RuleFor(a => a.FundamentacaoProblema)
            .InclusiveBetween(0, 5).WithMessage("Fundamentação do problema deve estar entre 0 e 5");

        // Defesa da solução (0-5)
        RuleFor(a => a.FocoSolucao)
            .InclusiveBetween(0, 5).WithMessage("Foco na solução deve estar entre 0 e 5");

        RuleFor(a => a.ViabilidadeSolucao)
            .InclusiveBetween(0, 5).WithMessage("Viabilidade da solução deve estar entre 0 e 5");

        // Apresentação (0-5)
        RuleFor(a => a.ClarezaApresentacao)
            .InclusiveBetween(0, 5).WithMessage("Clareza da apresentação deve estar entre 0 e 5");

        RuleFor(a => a.DominioAssunto)
            .InclusiveBetween(0, 5).WithMessage("Domínio do assunto deve estar entre 0 e 5");

        RuleFor(a => a.TransmissaoInformacoes)
            .InclusiveBetween(0, 5).WithMessage("Transmissão de informações deve estar entre 0 e 5");

        RuleFor(a => a.PadronizacaoApresentacao)
            .InclusiveBetween(0, 5).WithMessage("Padronização da apresentação deve estar entre 0 e 5");

        RuleFor(a => a.LinguagemTempo)
            .InclusiveBetween(0, 5).WithMessage("Linguagem e tempo deve estar entre 0 e 5");

        RuleFor(a => a.QualidadeRespostas)
            .InclusiveBetween(0, 5).WithMessage("Qualidade das respostas deve estar entre 0 e 5");

        RuleFor(a => a.Comentarios)
            .MaximumLength(2000).WithMessage("Comentários deve ter no máximo 2000 caracteres")
            .When(a => !string.IsNullOrWhiteSpace(a.Comentarios));
    }
}
