using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PeiFeira.Api.Filters;
using PeiFeira.Application.Services;
using PeiFeira.Application.Services.DisciplinasPI;
using PeiFeira.Application.Services.Equipes;
using PeiFeira.Application.Services.Matriculas;
using PeiFeira.Application.Services.Semestres;
using PeiFeira.Application.Services.Turmas;
using PeiFeira.Application.Services.Usuarios;
using PeiFeira.Application.Services.Usuarios.Services;
using PeiFeira.Application.Services.Usuarios.Services.PerfilCreation;
using PeiFeira.Domain.Interfaces.Repositories;
using PeiFeira.Infrastructure.Data;
using PeiFeira.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// AppServices
builder.Services.AddScoped<UsuarioAppService>();
builder.Services.AddScoped<DisciplinaPIAppService>();
builder.Services.AddScoped<SemestreAppService>();
builder.Services.AddScoped<TurmaAppService>();
builder.Services.AddScoped<IMatriculaTransactionService, MatriculaTransactionService>();
builder.Services.AddScoped<MatriculaAppService>();
builder.Services.AddScoped<EquipeAppService>();

// Strategy Pattern
builder.Services.AddScoped<IPerfilCreationStrategy, AlunoPerfilCreationStrategy>();
builder.Services.AddScoped<IPerfilCreationStrategy, ProfessorPerfilCreationStrategy>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();