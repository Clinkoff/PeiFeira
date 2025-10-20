using FluentValidation;
using PeiFeira.Communication.Requests.Semestres;

namespace PeiFeira.Application.Validators.Semestre;

public class UpdateSemestreRequestValidator : BaseValidator<UpdateSemestreRequest>
{
    public UpdateSemestreRequestValidator()
    {
        RuleFor(s => s.Nome)
            .NotEmpty().WithMessage("Nome do semestre é obrigatório")
            .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres");

        RuleFor(s => s.DataInicio)
            .NotEmpty().WithMessage("Data de início é obrigatória")
            .LessThan(s => s.DataFim).WithMessage("Data de início deve ser anterior à data de fim");

        RuleFor(s => s.DataFim)
            .NotEmpty().WithMessage("Data de fim é obrigatória")
            .GreaterThan(s => s.DataInicio).WithMessage("Data de fim deve ser posterior à data de início");
    }
}