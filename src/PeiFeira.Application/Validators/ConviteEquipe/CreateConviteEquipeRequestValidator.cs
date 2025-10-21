using FluentValidation;
using PeiFeira.Communication.Requests.ConviteEquipe;

namespace PeiFeira.Application.Validators.ConviteEquipe;

public class CreateConviteEquipeRequestValidator : BaseValidator<CreateConviteEquipeRequest>
{
    public CreateConviteEquipeRequestValidator()
    {
        RuleFor(c => c.EquipeId)
            .NotEmpty().WithMessage("EquipeId é obrigatório")
            .Must(BeValidGuid).WithMessage("EquipeId inválido");

        RuleFor(c => c.ConvidadoPorId)
            .NotEmpty().WithMessage("ConvidadoPorId é obrigatório")
            .Must(BeValidGuid).WithMessage("ConvidadoPorId inválido");

        RuleFor(c => c.ConvidadoId)
            .NotEmpty().WithMessage("ConvidadoId é obrigatório")
            .Must(BeValidGuid).WithMessage("ConvidadoId inválido");

        RuleFor(c => c.Mensagem)
            .MaximumLength(500).WithMessage("Mensagem deve ter no máximo 500 caracteres")
            .When(c => !string.IsNullOrWhiteSpace(c.Mensagem));
    }
}
