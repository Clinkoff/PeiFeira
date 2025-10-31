using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Enums;
using PeiFeira.Infrastructure.Data;
using PeiFeira.Infrastructure.Security;

namespace PeiFeira.Api.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PeiFeiraDbContext>();

        // Aplicar migrations pendentes (se houver)
        await context.Database.MigrateAsync();

        // Seed Admin
        await SeedAdminAsync(context);
    }

    private static async Task SeedAdminAsync(PeiFeiraDbContext context)
    {
        // Verificar se já existe algum admin
        var adminExists = await context.Usuarios.AnyAsync(u => u.Role == UserRole.Admin);

        if (adminExists)
        {
            Console.WriteLine("ℹ️  Admin já existe no banco");
            return;
        }
        var passwordHasher = new PasswordHasher();
        var adminUser = new Usuario
        {
            Id = Guid.NewGuid(),
            Matricula = "admin",
            Nome = "Administrador do Sistema",
            Email = "admin@peifeira.com",
            SenhaHash = passwordHasher.HashPassword("Admin@123"),
            Role = UserRole.Admin,
            IsActive = true
        };

        context.Usuarios.Add(adminUser);
        await context.SaveChangesAsync();

        Console.WriteLine("╔════════════════════════════════════════════╗");
        Console.WriteLine("║  ✅ ADMIN PADRÃO CRIADO COM SUCESSO!      ║");
        Console.WriteLine("╠════════════════════════════════════════════╣");
        Console.WriteLine("║  Matrícula: admin                          ║");
        Console.WriteLine("║  Senha:     Admin@123                      ║");
        Console.WriteLine("║  Email:     admin@peifeira.com             ║");
        Console.WriteLine("╚════════════════════════════════════════════╝");
    }

    // ✅ OPCIONAL: Seed de semestre/turma para facilitar testes
    public static async Task SeedTestDataAsync(PeiFeiraDbContext context)
    {
        // Você pode adicionar dados de teste aqui depois
        // Exemplo: criar um semestre 2025.1, uma turma, etc.
    }
}