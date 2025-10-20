using FluentValidation;
using PeiFeira.Communication.Requests.Semestres;

namespace PeiFeira.Application.Validators.Semestre;

public class CreateSemestreRequestValidator : BaseValidator<CreateSemestreRequest>
{
    public CreateSemestreRequestValidator()
    {
        RuleFor(s => s.Nome)
            .NotEmpty().WithMessage("Nome do semestre é obrigatório")
            .MaximumLength(10).WithMessage("Nome deve ter entre 3 e 100 caracteres");

        RuleFor(s => s.Ano)
            .NotEmpty().WithMessage("Ano é obrigatório")
            .GreaterThanOrEqualTo(2020).WithMessage("Ano deve ser maior ou igual a 2020")
            .LessThanOrEqualTo(2100).WithMessage("Ano inválido");

        RuleFor(s => s.Periodo)
            .NotEmpty().WithMessage("Período é obrigatório")
            .InclusiveBetween(1, 2).WithMessage("Período deve ser 1 ou 2");

        RuleFor(s => s.DataInicio)
            .NotEmpty().WithMessage("Data de início é obrigatória")
            .LessThan(s => s.DataFim).WithMessage("Data de início deve ser anterior à data de fim");

        RuleFor(s => s.DataFim)
            .NotEmpty().WithMessage("Data de fim é obrigatória")
            .GreaterThan(s => s.DataInicio).WithMessage("Data de fim deve ser posterior à data de início");
    }
}