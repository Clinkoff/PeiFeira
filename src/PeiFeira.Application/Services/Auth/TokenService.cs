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
        var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "1440");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("userId", usuario.Id.ToString()),
            new Claim("email", usuario.Email),
            new Claim("matricula", usuario.Matricula),
            new Claim("role", usuario.Role.ToString()), 
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (usuario.Role == UserRole.Aluno && usuario.PerfilAluno != null)
        {
            claims.Add(new Claim("perfilId", usuario.PerfilAluno.Id.ToString()));
        }
        else if (usuario.Role == UserRole.Professor && usuario.PerfilProfessor != null)
        {
            claims.Add(new Claim("perfilId", usuario.PerfilProfessor.Id.ToString()));
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
        var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "1440");
        return expirationMinutes * 60;
    }
}