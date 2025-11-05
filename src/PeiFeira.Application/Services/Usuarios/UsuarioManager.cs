using PeiFeira.Application.Services.Usuarios.Services;
using PeiFeira.Application.Services.Usuarios.Services.PerfilCreation;
using PeiFeira.Communication.Enums;
using PeiFeira.Communication.Requests.Auth;
using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Communication.Responses.Usuario;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Interfaces.Repositories;

namespace PeiFeira.Application.Services.Usuarios;

public class UsuarioManager : IUsuarioManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioValidator _usuarioValidator;
    private readonly IPasswordService _passwordService;
    private readonly PerfilCreationService _perfilCreationService;

    public UsuarioManager(
        IUnitOfWork unitOfWork,
        IUsuarioValidator usuarioValidator,
        IPasswordService passwordService,
        PerfilCreationService perfilCreationService)
    {
        _unitOfWork = unitOfWork;
        _usuarioValidator = usuarioValidator;
        _passwordService = passwordService;
        _perfilCreationService = perfilCreationService;
    }

    public async Task<UsuarioResponse> CreateAsync(CreateUsuarioRequest request)
    {
        await _usuarioValidator.ValidateCreateRequestAsync(request);
        await _usuarioValidator.ValidateUniquenessAsync(request.Matricula, request.Email);

        var usuario = CreateUsuarioFromRequest(request);
        var created = await _unitOfWork.Usuarios.CreateAsync(usuario);

        await _perfilCreationService.CreatePerfilAsync(usuario, request);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(created);
    }

    private Usuario CreateUsuarioFromRequest(CreateUsuarioRequest request)
    {
        return new Usuario
        {
            Matricula = request.Matricula,
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = _passwordService.HashPassword(request.Senha), // DIP: Abstração de hash
            Role = (UserRole)request.Role // Conversão entre enums
        };
    }

    public async Task<UsuarioResponse> UpdateAsync(Guid id, UpdateUsuarioRequest request)
    {
        await _usuarioValidator.ValidateUpdateRequestAsync(request);

        var usuario = await _unitOfWork.Usuarios.GetByIdWithPerfilAsync(id);
        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado");

        if (request.Email != usuario.Email && await _unitOfWork.Usuarios.ExistsByEmailAsync(request.Email))
            throw new InvalidOperationException("Email já existe");

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;

        var updated = await _unitOfWork.Usuarios.UpdateAsync(usuario);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _unitOfWork.Usuarios.SoftDeleteAsync(id);
        if (result)
            await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<UsuarioResponse?> GetByIdAsync(Guid id)
    {
        var usuario = await _unitOfWork.Usuarios.GetByIdWithPerfilAsync(id);
        return usuario != null ? MapToResponse(usuario) : null;
    }

    public async Task<UsuarioResponse?> GetByMatriculaAsync(string matricula)
    {
        var usuario = await _unitOfWork.Usuarios.GetByMatriculaWithPerfilAsync(matricula);
        return usuario != null ? MapToResponse(usuario) : null;
    }

    public async Task<UsuarioResponse?> GetByEmailAsync(string email)
    {
        var usuario = await _unitOfWork.Usuarios.GetByEmailAsync(email);
        return usuario != null ? MapToResponse(usuario) : null;
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAllAsync()
    {
        var usuarios = await _unitOfWork.Usuarios.GetAllAsync();
        return usuarios.Select(MapToResponse);
    }

    public async Task<IEnumerable<UsuarioResponse>> GetActiveAsync()
    {
        var usuarios = await _unitOfWork.Usuarios.GetActiveAsync();
        return usuarios.Select(MapToResponse);
    }

    public async Task<IEnumerable<UsuarioResponse>> GetProfessoresAsync()
    {
        var usuarios = await _unitOfWork.Usuarios.GetProfessoresAsync();
        return usuarios.Select(MapToResponse);
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAlunosAsync()
    {
        var usuarios = await _unitOfWork.Usuarios.GetAlunosAsync();
        return usuarios.Select(MapToResponse);
    }

    public async Task<UsuarioResponse?> LoginAsync(LoginRequest request)
    {
        await _usuarioValidator.ValidateLoginRequestAsync(request);

        var usuario = await _unitOfWork.Usuarios.GetByMatriculaWithPerfilAsync(request.Matricula);
        if (usuario == null || !usuario.IsActive)
            return null;

        if (!_passwordService.VerifyPassword(request.Senha, usuario.SenhaHash))
            return null;

        return MapToResponse(usuario);
    }

    public async Task<bool> MudarSenhaAsync(Guid id, MudarSenhaRequest request)
    {
        await _usuarioValidator.ValidateMudarSenhaRequestAsync(request);

        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
        if (usuario == null)
            return false;

        if (!_passwordService.VerifyPassword(request.SenhaAtual, usuario.SenhaHash))
            throw new UnauthorizedAccessException("Senha atual incorreta");

        usuario.SenhaHash = _passwordService.HashPassword(request.NovaSenha);
        await _unitOfWork.Usuarios.UpdateAsync(usuario);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExistsByMatriculaAsync(string matricula)
    {
        return await _unitOfWork.Usuarios.ExistsByMatriculaAsync(matricula);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _unitOfWork.Usuarios.ExistsByEmailAsync(email);
    }
    public async Task<IEnumerable<UsuarioResponse>> GetAlunosAtivosAsync()
    {
        // Busca todos os usuários que possuem perfil de aluno e estão ativos
        var usuarios = await _unitOfWork.Usuarios.GetAlunosAtivosAsync();

        return usuarios.Select(MapToResponse);
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAlunosDisponiveisAsync(Guid turmaId)
    {
        var alunos = await _unitOfWork.Usuarios.GetAlunosDisponiveisAsync(turmaId);
        return alunos.Select(MapToResponse);
    }
    private static UsuarioResponse MapToResponse(Usuario usuario)
    {
        return new UsuarioResponse
        {
            Id = usuario.Id,
            IsActive = usuario.IsActive,
            Matricula = usuario.Matricula,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Role = (UserRoleDto)usuario.Role,
            CriadoEm = usuario.CriadoEm,
            AlteradoEm = usuario.AlteradoEm,

            PerfilAluno = usuario.PerfilAluno != null ? new PerfilAlunoResponse
             {
                 Id = usuario.PerfilAluno.Id,
                 Curso = usuario.PerfilAluno.Curso,
                 Turno = usuario.PerfilAluno.Turno
             } : null,

            PerfilProfessor = usuario.PerfilProfessor != null ? new PerfilProfessorResponse
            {
                Id = usuario.PerfilProfessor.Id,
                Departamento = usuario.PerfilProfessor.Departamento,
                Titulacao = usuario.PerfilProfessor.Titulacao,
                AreaEspecializacao = usuario.PerfilProfessor.AreaEspecializacao
            } : null
        };
    }
}