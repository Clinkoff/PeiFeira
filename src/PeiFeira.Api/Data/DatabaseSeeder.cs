using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Infrastructure.Data;
using PeiFeira.Infrastructure.Security;

namespace PeiFeira.Api.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAdminAsync(PeiFeiraDbContext context)
    {
        if (context.Usuarios.Any(u => u.Role == UserRole.Admin))
        {
            return; 
        }

        var adminUser = new Usuario
        {
            Id = Guid.NewGuid(),
            Matricula = "admin",
            Nome = "Administrador",
            Email = "admin@peifeira.com",
            SenhaHash = new PasswordHasher().HashPassword("Admin@123"), // Senha padrão
            Role = UserRole.Admin,
            IsActive = true
        };

        context.Usuarios.Add(adminUser);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ Admin padrão criado:");
        Console.WriteLine($"   Matrícula: {adminUser.Matricula}");
        Console.WriteLine($"   Senha: Admin@123");
    }
}