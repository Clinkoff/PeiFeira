using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Interfaces;

namespace PeiFeira.Application.Services.Usuarios.Services.PerfilCreation;

public class AlunoPerfilCreationStrategy : IPerfilCreationStrategy
{
    private readonly IUnitOfWork _unitOfWork;

    public AlunoPerfilCreationStrategy(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CanHandle(UserRole role)
    {
        return role == UserRole.Aluno;
    }

    public async Task CreatePerfilAsync(Usuario usuario, CreateUsuarioRequest request)
    {
        if (request.PerfilAluno == null)
            throw new ArgumentException("Dados do perfil de aluno são obrigatórios");

        var perfilAluno = new PerfilAluno
        {
            Usuario = usuario,
            Curso = request.PerfilAluno.Curso!,
            Semestre = request.PerfilAluno.Semestre!,
            Periodo = request.PerfilAluno.Periodo!
        };

        usuario.PerfilAluno = perfilAluno;
        await _unitOfWork.PerfisAluno.CreateAsync(perfilAluno);
    }
}