using FluentValidation;
using PeiFeira.Communication.Requests.Usuario;

namespace PeiFeira.Application.Validators.Usuarios;

public class CreateUsuarioRequestValidator : BaseValidator<CreateUsuarioRequest>
{
    public CreateUsuarioRequestValidator()
    {
        RuleFor(x => x.Matricula)
            .NotEmpty()
            .WithMessage("Matrícula é obrigatória")
            .Must(BeValidMatricula)
            .WithMessage("Matrícula deve ter 10 dígitos");

        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .Length(2, 200)
            .WithMessage("Nome deve ter entre 2 e 200 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório")
            .Must(BeValidEmail)
            .WithMessage("Email deve ser válido");

        RuleFor(x => x.Senha)
            .NotEmpty()
            .WithMessage("Senha é obrigatória")
            .MinimumLength(6)
            .WithMessage("Senha deve ter pelo menos 6 caracteres");

        RuleFor(x => x.Role)
            .Must(r => r == 1 || r == 2)
            .WithMessage("Role deve ser 1 (Aluno) ou 2 (Professor)");
    }
}