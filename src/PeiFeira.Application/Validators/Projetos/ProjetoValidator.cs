using FluentValidation;
using PeiFeira.Domain.Entities.Projetos;

namespace PeiFeira.Application.Validators.Projetos;

public class ProjetoValidator : BaseValidator<Projeto>
{
    public ProjetoValidator()
    {
        RuleFor(p => p.Titulo)
            .NotEmpty()
            .WithMessage("Título do projeto é obrigatório")
            .Length(5, 300)
            .WithMessage("Título deve ter entre 5 e 300 caracteres");

        RuleFor(p => p.Tema)
            .NotEmpty()
            .WithMessage("Tema do projeto é obrigatório")
            .Length(5, 500)
            .WithMessage("Tema deve ter entre 5 e 500 caracteres");

        RuleFor(p => p.DesafioProposto)
            .NotEmpty()
            .WithMessage("Desafio proposto é obrigatório")
            .Length(10, 1000)
            .WithMessage("Desafio deve ter entre 10 e 1000 caracteres");

        RuleFor(p => p.EquipeId)
            .NotEmpty()
            .WithMessage("Equipe é obrigatória")
            .Must(BeValidGuid)
            .WithMessage("ID da equipe deve ser válido");

   
    }
}