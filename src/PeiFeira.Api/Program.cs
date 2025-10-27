﻿using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PeiFeira.Api.Filters;
using PeiFeira.Application.Services;
using PeiFeira.Application.Services.Auth;
using PeiFeira.Application.Services.Avaliacoes;
using PeiFeira.Application.Services.ConviteEquipe;
using PeiFeira.Application.Services.DisciplinasPI;
using PeiFeira.Application.Services.Equipes;
using PeiFeira.Application.Services.Matriculas;
using PeiFeira.Application.Services.Matriculas.Services;
using PeiFeira.Application.Services.MembrosEquipes;
using PeiFeira.Application.Services.Projetos;
using PeiFeira.Application.Services.Semestres;
using PeiFeira.Application.Services.Turmas;
using PeiFeira.Application.Services.Usuarios;
using PeiFeira.Application.Services.Usuarios.Services;
using PeiFeira.Application.Services.Usuarios.Services.PerfilCreation;
using PeiFeira.Application.Validators.Auth;
using PeiFeira.Domain.Interfaces.Auth;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Infrastructure.Data;
using PeiFeira.Infrastructure.Repositories;
using PeiFeira.Infrastructure.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

// Database
builder.Services.AddDbContext<PeiFeiraDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ========== DEPENDENCY INJECTION ==========

// Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// FluentValidation - Registra AbstractValidator<T> automaticamente
builder.Services.AddValidatorsFromAssemblyContaining<ValidationService>();

// ValidationService (classe concreta)
builder.Services.AddScoped<ValidationService>();

// Services customizados
builder.Services.AddScoped<IUsuarioValidator, UsuarioValidatorService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<PerfilCreationService>();

// Managers
builder.Services.AddScoped<IUsuarioManager, UsuarioManager>();
builder.Services.AddScoped<IDisciplinaPIManager, DisciplinaPIManager>();
builder.Services.AddScoped<ISemestreManager, SemestreManager>();
builder.Services.AddScoped<ITurmaManager, TurmaManager>();
builder.Services.AddScoped<IMatriculaManager, MatriculaManager>();
builder.Services.AddScoped<IEquipeManager, EquipeManager>();
builder.Services.AddScoped<IMembroEquipeManager, MembroEquipeManager>();
builder.Services.AddScoped<IConviteEquipeManager, ConviteEquipeManager>();
builder.Services.AddScoped<IProjetoManager, ProjetoManager>();
builder.Services.AddScoped<IAvaliacaoManager, AvaliacaoManager>();


// AppServices
builder.Services.AddScoped<UsuarioAppService>();
builder.Services.AddScoped<DisciplinaPIAppService>();
builder.Services.AddScoped<SemestreAppService>();
builder.Services.AddScoped<TurmaAppService>();
builder.Services.AddScoped<IMatriculaTransactionService, MatriculaTransactionService>();
builder.Services.AddScoped<MatriculaAppService>();
builder.Services.AddScoped<EquipeAppService>();
builder.Services.AddScoped<MembroEquipeAppService>();
builder.Services.AddScoped<ConviteEquipeAppService>();
builder.Services.AddScoped<ProjetoAppService>();
builder.Services.AddScoped<AvaliacaoAppService>();

// Strategy Pattern
builder.Services.AddScoped<IPerfilCreationStrategy, AlunoPerfilCreationStrategy>();
builder.Services.AddScoped<IPerfilCreationStrategy, ProfessorPerfilCreationStrategy>();


//Auth
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<AuthAppService>();

var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("Jwt:Secret não configurado");
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ClockSkew = TimeSpan.Zero // Remove delay padrão de 5min na expiração
    };
});

// ✅ CONFIGURAR AUTORIZAÇÃO COM POLICIES
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("role", "Admin"));

    options.AddPolicy("ProfessorOnly", policy =>
        policy.RequireClaim("role", "Professor"));

    options.AddPolicy("AlunoOnly", policy =>
        policy.RequireClaim("role", "Aluno"));

    options.AddPolicy("ProfessorOrAdmin", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "role" &&
                (c.Value == "Professor" || c.Value == "Admin"))));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Ele valida o token primeiro.
app.UseAuthorization();
app.MapControllers();

app.Run();