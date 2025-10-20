using FluentValidation;
using PeiFeira.Communication.Requests.Matriculas;
using PeiFeira.Domain.Interfaces.Repositories;

namespace PeiFeira.Application.Validators.Matriculas;

public class MatriculaBusinessValidator : AbstractValidator<CreateMatriculaRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculaBusinessValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(m => m)
            .MustAsync(NotMatriculadoNaTurmaAsync)
            .WithMessage("Aluno já está matriculado nesta turma")
            .WithErrorCode("CONFLICT");
    }

    private async Task<bool> NotMatriculadoNaTurmaAsync(CreateMatriculaRequest request, CancellationToken cancellation)
    {
        return !await _unitOfWork.AlunoTurmas.ExistsMatriculaAtivaAsync(
            request.PerfilAlunoId,
            request.TurmaId
        );
    }
}