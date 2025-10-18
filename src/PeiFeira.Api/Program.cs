using Microsoft.EntityFrameworkCore;
using FluentValidation;
using PeiFeira.Infrastructure.Data;
using PeiFeira.Infrastructure.Repositories;
using PeiFeira.Application.Services;
using PeiFeira.Application.Services.Usuarios;
using PeiFeira.Application.Services.Usuarios.Services;
using PeiFeira.Application.Services.Usuarios.Services.PerfilCreation;
using PeiFeira.Application.Services.DisciplinasPI;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Domain.Interfaces.DisciplinasPI;
using PeiFeira.Domain.Interfaces.Usuarios;
using PeiFeira.Api.Filters;
using PeiFeira.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<PeiFeiraDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
//builder.Services.AddScoped<IEquipeRepository, EquipeRepository>();
//builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
//builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
//builder.Services.AddScoped<IConviteEquipeRepository, ConviteEquipeRepository>();
builder.Services.AddScoped<IPerfilAlunoRepository, PerfilAlunoRepository>();
builder.Services.AddScoped<IPerfilProfessorRepository, PerfilProfessorRepository>();
builder.Services.AddScoped<IDisciplinaPIRepository, DisciplinaPIRepository>();
builder.Services.AddScoped<IDisciplinaPITurmaRepository, DisciplinaPITurmaRepository>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application Services
builder.Services.AddScoped<ValidationService>();

// SOLID Services
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IUsuarioValidator, UsuarioValidatorService>();
builder.Services.AddScoped<PerfilCreationService>();
builder.Services.AddScoped<IPerfilCreationStrategy, AlunoPerfilCreationStrategy>();
builder.Services.AddScoped<IPerfilCreationStrategy, ProfessorPerfilCreationStrategy>();

builder.Services.AddScoped<UsuarioManager>();
builder.Services.AddScoped<UsuarioAppService>();

// DisciplinaPI Services
builder.Services.AddScoped<IDisciplinaPIManager, DisciplinaPIManager>();
builder.Services.AddScoped<DisciplinaPIAppService>();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ValidationService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();