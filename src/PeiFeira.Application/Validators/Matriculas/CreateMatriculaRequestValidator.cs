using FluentValidation;
using PeiFeira.Application.Validators;
using PeiFeira.Communication.Requests.Matriculas;
using PeiFeira.Domain.Interfaces.Repositories;

public class CreateMatriculaRequestValidator : BaseValidator<CreateMatriculaRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMatriculaRequestValidator(IUnitOfWork unitOfWork) 
    {
        _unitOfWork = unitOfWork;

        RuleFor(m => m.TurmaId)
            .NotEmpty().WithMessage("TurmaId é obrigatório")
            .MustAsync(BeValidTurmaAsync).WithMessage("Turma não encontrada");

        RuleFor(m => m.PerfilAlunoId)
            .NotEmpty().WithMessage("PerfilAlunoId é obrigatório")
            .MustAsync(BeValidAlunoAsync).WithMessage("Aluno não encontrado");
    }

    private async Task<bool> BeValidTurmaAsync(Guid turmaId, CancellationToken cancellation)
    {
        var turma = await _unitOfWork.Turmas.GetByIdAsync(turmaId);
        return turma != null;
    }

    private async Task<bool> BeValidAlunoAsync(Guid perfilAlunoId, CancellationToken cancellation)
    {
        var aluno = await _unitOfWork.PerfisAluno.GetByIdAsync(perfilAlunoId);
        return aluno != null;
    }
}