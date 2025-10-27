using FluentValidation;
using PeiFeira.Application.Validators.Auth;
using PeiFeira.Communication.Requests.Auth;
using PeiFeira.Communication.Responses.Auth;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Interfaces.Auth;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Exception.ExeceptionsBases;

namespace PeiFeira.Application.Services.Auth;

public class AuthManager : IAuthManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginRequest> _loginValidator; 
    private readonly IPasswordHasher _passwordHasher;

    public AuthManager(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IValidator<LoginRequest> loginValidator,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _loginValidator = loginValidator;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        await _loginValidator.ValidateAndThrowAsync(request);

        var usuario = await _unitOfWork.Usuarios.GetByMatriculaWithPerfilAsync(request.Matricula);

        if (usuario == null)
        {
            throw new UnauthorizedException("Matrícula ou senha incorreta");
        }

        if (!usuario.IsActive)
        {
            throw new UnauthorizedException("Usuário inativo");
        }

        bool senhaCorreta = _passwordHasher.VerifyPassword(request.Senha, usuario.SenhaHash);

        if (!senhaCorreta)
        {
            throw new UnauthorizedException("Matrícula ou senha incorreta");
        }

        // 5. Gerar token JWT
        var token = _tokenService.GenerateToken(usuario);
        var expirationSeconds = _tokenService.GetExpirationInSeconds();

        // 6. Montar resposta
        return new LoginResponse
        {
            Token = token,
            ExpiresIn = expirationSeconds,
            Usuario = MapToUserInfo(usuario) // ← usar método
        };
    }

    public async Task<UserInfo> GetUserInfoAsync(Guid userId)
    {
        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(userId);

        if (usuario == null)
        {
            throw new NotFoundException("Usuario", userId);
        }

        return MapToUserInfo(usuario);
    }

    private static UserInfo MapToUserInfo(Usuario usuario)
    {
        return new UserInfo
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Matricula = usuario.Matricula,
            Tipo = usuario.Role.ToString(),
            PerfilId = GetPerfilId(usuario)
        };
    }

    private static Guid? GetPerfilId(Usuario usuario)
    {
        return usuario.Role switch
        {
            UserRole.Aluno => usuario.PerfilAluno?.Id,
            UserRole.Professor => usuario.PerfilProfessor?.Id,
            UserRole.Coordenador => usuario.PerfilProfessor?.Id,
            _ => null
        };
    }
}