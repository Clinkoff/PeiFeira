using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Interfaces;

namespace PeiFeira.Application.Services.Usuarios.Services.PerfilCreation;

public class ProfessorPerfilCreationStrategy : IPerfilCreationStrategy
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfessorPerfilCreationStrategy(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CanHandle(UserRole role)
    {
        return role == UserRole.Professor;
    }

    public async Task CreatePerfilAsync(Usuario usuario, CreateUsuarioRequest request)
    {
        if (request.PerfilProfessor == null)
            throw new ArgumentException("Dados do perfil de professor são obrigatórios");

        var perfilProfessor = new PerfilProfessor
        {
            Usuario = usuario,
            Departamento = request.PerfilProfessor.Departamento!,
            Titulacao = request.PerfilProfessor.Titulacao!,
            AreaEspecializacao = request.PerfilProfessor.AreaEspecializacao!
        };

        usuario.PerfilProfessor = perfilProfessor;
        await _unitOfWork.PerfisProfessor.CreateAsync(perfilProfessor);
    }
}