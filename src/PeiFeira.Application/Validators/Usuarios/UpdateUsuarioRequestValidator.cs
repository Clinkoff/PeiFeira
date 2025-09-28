using FluentValidation;
using PeiFeira.Communication.Requests.Usuario;

namespace PeiFeira.Application.Validators.Usuarios;

public class UpdateUsuarioRequestValidator : BaseValidator<UpdateUsuarioRequest>
{
    public UpdateUsuarioRequestValidator()
    {
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

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Função '{PropertyValue}' é inválida");
    }
}