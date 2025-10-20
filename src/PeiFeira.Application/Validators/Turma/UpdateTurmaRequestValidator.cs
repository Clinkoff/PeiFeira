using FluentValidation;
using PeiFeira.Communication.Requests.Turma;

namespace PeiFeira.Application.Validators.Turma;

public class UpdateTurmaRequestValidator : BaseValidator<UpdateTurmaRequest>
{
    public UpdateTurmaRequestValidator()
    {
        RuleFor(t => t.Nome)
            .NotEmpty().WithMessage("Nome da turma é obrigatório")
            .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres");

        RuleFor(t => t.Curso)
            .MaximumLength(100).WithMessage("Curso deve ter no máximo 100 caracteres")
            .When(t => !string.IsNullOrWhiteSpace(t.Curso));

        RuleFor(t => t.Periodo)
            .GreaterThan(0).WithMessage("Período deve ser maior que 0")
            .LessThanOrEqualTo(10).WithMessage("Período deve ser menor ou igual a 10")
            .When(t => t.Periodo.HasValue);

        RuleFor(t => t.Turno)
            .MaximumLength(20).WithMessage("Turno deve ter no máximo 20 caracteres")
            .When(t => !string.IsNullOrWhiteSpace(t.Turno));
    }
}
