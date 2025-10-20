using PeiFeira.Domain.Entities.Turmas;
using PeiFeira.Domain.Interfaces.Repositories;

namespace PeiFeira.Application.Services.Matriculas;

public interface IMatriculaTransactionService
{
    Task<Guid> CreateMatriculaAsync(Guid turmaId, Guid perfilAlunoId);
}

public class MatriculaTransactionService : IMatriculaTransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculaTransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateMatriculaAsync(Guid turmaId, Guid perfilAlunoId)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Desativar matrícula atual
            var matriculaAtual = await _unitOfWork.AlunoTurmas
                .GetMatriculaAtualByPerfilAlunoIdAsync(perfilAlunoId);

            if (matriculaAtual != null)
            {
                matriculaAtual.IsAtual = false;
                await _unitOfWork.AlunoTurmas.UpdateAsync(matriculaAtual);
            }

            // Criar nova matrícula
            var novaMatricula = new AlunoTurma
            {
                TurmaId = turmaId,
                PerfilAlunoId = perfilAlunoId,
                DataMatricula = DateTime.UtcNow,
                IsAtual = true
            };

            await _unitOfWork.AlunoTurmas.CreateAsync(novaMatricula);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return novaMatricula.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}