using FluentValidation;
using PeiFeira.Communication.Requests.MembroEquipe;

namespace PeiFeira.Application.Validators.MembroEquipe;

public class AddMembroEquipeRequestValidator : BaseValidator<AddMembroEquipeRequest>
{
    public AddMembroEquipeRequestValidator()
    {
        RuleFor(m => m.EquipeId)
            .NotEmpty().WithMessage("EquipeId é obrigatório")
            .Must(BeValidGuid).WithMessage("EquipeId inválido");

        RuleFor(m => m.PerfilAlunoId)
            .NotEmpty().WithMessage("PerfilAlunoId é obrigatório")
            .Must(BeValidGuid).WithMessage("PerfilAlunoId inválido");
    }
}
