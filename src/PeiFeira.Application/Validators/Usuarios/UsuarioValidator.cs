using FluentValidation;
using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Application.Validators.Usuarios;

public class UsuarioValidator : BaseValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Matricula)
            .NotEmpty()
            .WithMessage("Matrícula é obrigatória")
            .Must(BeValidMatricula)
            .WithMessage("Matrícula deve ter 10 dígitos");

        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .Length(2, 200)
            .WithMessage("Nome deve ter entre 2 e 200 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório")
            .Must(BeValidEmail)
            .WithMessage("Email deve ser válido");

        RuleFor(u => u.Role)
            .IsInEnum()
            .WithMessage("Role deve ser Aluno ou Professor");

        RuleFor(u => u.SenhaHash)
            .NotEmpty()
            .WithMessage("Senha é obrigatória");
    }
}
