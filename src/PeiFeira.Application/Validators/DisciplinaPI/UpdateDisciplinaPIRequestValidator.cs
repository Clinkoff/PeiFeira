using FluentValidation;
using PeiFeira.Communication.Requests.DisciplinaPI;

namespace PeiFeira.Application.Validators.DisciplinaPI;

public class UpdateDisciplinaPIRequestValidator : BaseValidator<UpdateDisciplinaPIRequest>
{
    public UpdateDisciplinaPIRequestValidator()
    {
        RuleFor(d => d.Nome)
            .NotEmpty()
            .WithMessage("Nome da disciplina é obrigatório")
            .Length(3, 200)
            .WithMessage("Nome deve ter entre 3 e 200 caracteres");

        RuleFor(d => d.TemaGeral)
            .NotEmpty()
            .WithMessage("Tema geral é obrigatório")
            .Length(5, 500)
            .WithMessage("Tema geral deve ter entre 5 e 500 caracteres");

        RuleFor(d => d.Descricao)
            .MaximumLength(2000)
            .When(d => !string.IsNullOrEmpty(d.Descricao))
            .WithMessage("Descrição não pode exceder 2000 caracteres");

        RuleFor(d => d.Objetivos)
            .MaximumLength(2000)
            .When(d => !string.IsNullOrEmpty(d.Objetivos))
            .WithMessage("Objetivos não podem exceder 2000 caracteres");

        RuleFor(d => d.DataInicio)
            .NotEmpty()
            .WithMessage("Data de início é obrigatória")
            .LessThan(d => d.DataFim)
            .WithMessage("Data de início deve ser anterior à data de fim");

        RuleFor(d => d.DataFim)
            .NotEmpty()
            .WithMessage("Data de fim é obrigatória")
            .GreaterThan(d => d.DataInicio)
            .WithMessage("Data de fim deve ser posterior à data de início");

        RuleFor(d => d.Status)
            .IsInEnum()
            .WithMessage("Status inválido");

        RuleFor(d => d.TurmaIds)
            .NotNull()
            .WithMessage("Lista de turmas não pode ser nula")
            .Must(turmas => turmas.Count > 0)
            .WithMessage("Ao menos uma turma deve ser associada")
            .Must(turmas => turmas.All(id => id != Guid.Empty))
            .WithMessage("IDs de turma inválidos");
    }
}
