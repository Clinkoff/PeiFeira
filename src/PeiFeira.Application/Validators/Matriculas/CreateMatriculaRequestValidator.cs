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

        RuleFor(m => m.UsuarioId)
            .NotEmpty().WithMessage("UsuarioId é obrigatório")
            .MustAsync(BeValidUsuarioAsync).WithMessage("Usuário não encontrado ou sem perfil de aluno.");
    }

    private async Task<bool> BeValidTurmaAsync(Guid turmaId, CancellationToken cancellation)
    {
        var turma = await _unitOfWork.Turmas.GetByIdAsync(turmaId);
        return turma != null;
    }

    private async Task<bool> BeValidUsuarioAsync(Guid usuarioId, CancellationToken cancellation)
    {
        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(usuarioId);
        if (usuario == null) return false;

        var perfisAluno = await _unitOfWork.PerfisAluno.GetAllAsync();
        var perfilAluno = perfisAluno.FirstOrDefault(p => p.UsuarioId == usuarioId);
        return perfilAluno != null;
    }
}
