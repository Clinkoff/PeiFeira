using FluentValidation;
using PeiFeira.Communication.Requests.Usuario;

namespace PeiFeira.Application.Validators.Usuarios;

public class LoginRequestValidator : BaseValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Matricula)
            .NotEmpty()
            .WithMessage("Matrícula é obrigatória")
            .Must(BeValidMatricula)
            .WithMessage("Matrícula deve ter 10 dígitos");

        RuleFor(x => x.SenhaHash)
            .NotEmpty()
            .WithMessage("Senha é obrigatória");
    }
}