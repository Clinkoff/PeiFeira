using FluentValidation;
using PeiFeira.Domain.Entities.Equipes;

namespace PeiFeira.Application.Validators.Equipes;


public class EquipeValidator : BaseValidator<Equipe>
{
    public EquipeValidator()
    {
        RuleFor(e => e.Nome)
            .NotEmpty()
            .WithMessage("Nome da equipe é obrigatório")
            .Length(2, 200)
            .WithMessage("Nome deve ter entre 2 e 200 caracteres");

        RuleFor(e => e.LiderId)
            .NotEmpty()
            .WithMessage("Líder da equipe é obrigatório")
            .Must(BeValidGuid)
            .WithMessage("ID do líder deve ser válido");
    }
}