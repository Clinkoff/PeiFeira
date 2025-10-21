using FluentValidation;
using PeiFeira.Communication.Requests.Equipes;

namespace PeiFeira.Application.Validators.Equipes;

public class UpdateEquipeRequestValidator : BaseValidator<UpdateEquipeRequest>
{
    public UpdateEquipeRequestValidator()
    {
        RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("Nome da equipe é obrigatório")
            .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres");
    }
}
