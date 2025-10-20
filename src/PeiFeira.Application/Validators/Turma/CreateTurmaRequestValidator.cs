using FluentValidation;
using PeiFeira.Communication.Requests.Turma;

namespace PeiFeira.Application.Validators.Turma;

public class CreateTurmaRequestValidator : BaseValidator<CreateTurmaRequest>
{
    public CreateTurmaRequestValidator()
    {
        RuleFor(t => t.SemestreId)
            .NotEmpty().WithMessage("SemestreId é obrigatório")
            .Must(BeValidGuid).WithMessage("SemestreId inválido");

        RuleFor(t => t.Nome)
            .NotEmpty().WithMessage("Nome da turma é obrigatório")
            .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres");

        RuleFor(t => t.Codigo)
            .NotEmpty().WithMessage("Código da turma é obrigatório")
            .Length(3, 20).WithMessage("Código deve ter entre 3 e 20 caracteres")
            .Matches(@"^[A-Z0-9-]+$").WithMessage("Código deve conter apenas letras maiúsculas, números e hífen");

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
