using FluentValidation;
using PeiFeira.Communication.Requests.Auth;

namespace PeiFeira.Application.Validators.Auth;

public class LoginRequestValidator : BaseValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Matricula)
            .NotEmpty().WithMessage("Matrícula é obrigatória")
            .MaximumLength(50).WithMessage("Matrícula desse tamanho não existe");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória")
            .MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres")
            .MaximumLength(72).WithMessage("Senha longa demais."); 
    }
}
