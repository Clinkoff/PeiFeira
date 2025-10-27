using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PeiFeira.Application.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Usuario usuario)
    {
        var secret = _configuration["Jwt:Secret"]
            ?? throw new InvalidOperationException("Jwt:Secret não configurado");
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        if (!int.TryParse(_configuration["Jwt:ExpirationMinutes"], out var expirationMinutes))
        {
            expirationMinutes = 1440;
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("userId", usuario.Id.ToString()),
                new Claim("email", usuario.Email),
            new Claim("matricula", usuario.Matricula),
            new Claim(ClaimTypes.Role, usuario.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var perfilId = usuario.Role switch
        {
            UserRole.Aluno when usuario.PerfilAluno != null => usuario.PerfilAluno.Id.ToString(),
            UserRole.Professor when usuario.PerfilProfessor != null => usuario.PerfilProfessor.Id.ToString(),
            UserRole.Coordenador when usuario.PerfilProfessor != null => usuario.PerfilProfessor.Id.ToString(),
            _ => null
        };

        if (perfilId != null)
        {
            claims.Add(new Claim("perfilId", perfilId));
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int GetExpirationInSeconds()
    {
        if (!int.TryParse(_configuration["Jwt:ExpirationMinutes"], out var expirationMinutes))
        {
            expirationMinutes = 1440; // Default 24 horas
        }
        return expirationMinutes * 60;
    }
}