using FluentValidation;
using PeiFeira.Communication.Requests.Usuario;

namespace PeiFeira.Application.Validators.Usuarios;

public class MudarSenhaRequestValidator : BaseValidator<MudarSenhaRequest>
{
    public MudarSenhaRequestValidator()
    {
        RuleFor(x => x.SenhaAtual)
            .NotEmpty()
            .WithMessage("Senha atual é obrigatória");

        RuleFor(x => x.NovaSenha)
            .NotEmpty()
            .WithMessage("Nova senha é obrigatória")
            .MinimumLength(6)
            .WithMessage("Nova senha deve ter pelo menos 6 caracteres");

        RuleFor(x => x.ConfirmarNovaSenha)
            .NotEmpty()
            .WithMessage("Confirmação da nova senha é obrigatória")
            .Equal(x => x.NovaSenha)
            .WithMessage("Confirmação da senha não confere");
    }
}