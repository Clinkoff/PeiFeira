using BCrypt.Net;
using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Communication.Responses.Usuario;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Interfaces;

namespace PeiFeira.Application.Services.Usuarios;

public class UsuarioManager : IUsuarioManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ValidationService _validationService;

    public UsuarioManager(IUnitOfWork unitOfWork, ValidationService validationService)
    {
        _unitOfWork = unitOfWork;
        _validationService = validationService;
    }

    public async Task<UsuarioResponse> CreateAsync(CreateUsuarioRequest request)
    {
        await _validationService.ValidateAsync(request);

        if (await _unitOfWork.Usuarios.ExistsByMatriculaAsync(request.Matricula))
            throw new InvalidOperationException("Matrícula já existe");

        if (await _unitOfWork.Usuarios.ExistsByEmailAsync(request.Email))
            throw new InvalidOperationException("Email já existe");

        var usuario = new Usuario
        {
            Matricula = request.Matricula,
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            Role = (UserRole)request.Role
        };

        var created = await _unitOfWork.Usuarios.CreateAsync(usuario);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(created);
    }

    public async Task<UsuarioResponse> UpdateAsync(Guid id, UpdateUsuarioRequest request)
    {
        await _validationService.ValidateAsync(request);

        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado");

        if (request.Email != usuario.Email && await _unitOfWork.Usuarios.ExistsByEmailAsync(request.Email))
            throw new InvalidOperationException("Email já existe");

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.Role = (UserRole)request.Role;

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
        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
        return usuario != null ? MapToResponse(usuario) : null;
    }

    public async Task<UsuarioResponse?> GetByMatriculaAsync(string matricula)
    {
        var usuario = await _unitOfWork.Usuarios.GetByMatriculaAsync(matricula);
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
        await _validationService.ValidateAsync(request);

        var usuario = await _unitOfWork.Usuarios.GetByMatriculaAsync(request.Matricula);
        if (usuario == null || !usuario.IsActive)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            return null;

        return MapToResponse(usuario);
    }

    public async Task<bool> MudarSenhaAsync(Guid id, MudarSenhaRequest request)
    {
        await _validationService.ValidateAsync(request);

        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
        if (usuario == null)
            return false;

        if (!BCrypt.Net.BCrypt.Verify(request.SenhaAtual, usuario.SenhaHash))
            throw new UnauthorizedAccessException("Senha atual incorreta");

        usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.NovaSenha);
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

    private static UsuarioResponse MapToResponse(Usuario usuario)
    {
        return new UsuarioResponse
        {
            Id = usuario.Id,
            IsActive = usuario.IsActive,
            Matricula = usuario.Matricula,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Role = usuario.Role == UserRole.Professor ? "Professor" : "Aluno",
            CriadoEm = usuario.CriadoEm,
            AlteradoEm = usuario.AlteradoEm
        };
    }
}