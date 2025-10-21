using FluentValidation;
using PeiFeira.Communication.Requests.Projetos;

namespace PeiFeira.Application.Validators.Projetos;

public class UpdateProjetoRequestValidator : BaseValidator<UpdateProjetoRequest>
{
    public UpdateProjetoRequestValidator()
    {
        RuleFor(p => p.Titulo)
            .NotEmpty().WithMessage("Título é obrigatório")
            .Length(5, 200).WithMessage("Título deve ter entre 5 e 200 caracteres");

        RuleFor(p => p.DesafioProposto)
            .NotEmpty().WithMessage("Desafio proposto é obrigatório")
            .Length(10, 2000).WithMessage("Desafio proposto deve ter entre 10 e 2000 caracteres");

        RuleFor(p => p.Status)
            .IsInEnum().WithMessage("Status inválido");

        // Validações opcionais
        RuleFor(p => p.NomeEmpresa)
            .MaximumLength(200).WithMessage("Nome da empresa deve ter no máximo 200 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.NomeEmpresa));

        RuleFor(p => p.EnderecoCompleto)
            .MaximumLength(500).WithMessage("Endereço completo deve ter no máximo 500 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.EnderecoCompleto));

        RuleFor(p => p.Cidade)
            .MaximumLength(100).WithMessage("Cidade deve ter no máximo 100 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.Cidade));

        RuleFor(p => p.RedeSocial)
            .MaximumLength(200).WithMessage("Rede social deve ter no máximo 200 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.RedeSocial));

        RuleFor(p => p.Contato)
            .MaximumLength(100).WithMessage("Contato deve ter no máximo 100 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.Contato));

        RuleFor(p => p.NomeResponsavel)
            .MaximumLength(200).WithMessage("Nome do responsável deve ter no máximo 200 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.NomeResponsavel));

        RuleFor(p => p.CargoResponsavel)
            .MaximumLength(100).WithMessage("Cargo do responsável deve ter no máximo 100 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.CargoResponsavel));

        RuleFor(p => p.TelefoneResponsavel)
            .MaximumLength(20).WithMessage("Telefone do responsável deve ter no máximo 20 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.TelefoneResponsavel));

        RuleFor(p => p.EmailResponsavel)
            .MaximumLength(200).WithMessage("Email do responsável deve ter no máximo 200 caracteres")
            .Must(BeValidEmail).WithMessage("Email do responsável inválido")
            .When(p => !string.IsNullOrWhiteSpace(p.EmailResponsavel));

        RuleFor(p => p.RedesSociaisResponsavel)
            .MaximumLength(500).WithMessage("Redes sociais do responsável deve ter no máximo 500 caracteres")
            .When(p => !string.IsNullOrWhiteSpace(p.RedesSociaisResponsavel));
    }
}
