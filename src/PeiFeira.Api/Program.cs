using Microsoft.EntityFrameworkCore;
using FluentValidation;
using PeiFeira.Infrastructure.Data;
using PeiFeira.Infrastructure.Repositories;
using PeiFeira.Application.Services;
using PeiFeira.Application.Services.Usuarios;
using PeiFeira.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
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
//builder.Services.AddScoped<IMembroEquipeRepository, MembroEquipeRepository>();
//builder.Services.AddScoped<IConviteEquipeRepository, ConviteEquipeRepository>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application Services
builder.Services.AddScoped<ValidationService>();
builder.Services.AddScoped<UsuarioManager>();
builder.Services.AddScoped<UsuarioAppService>();

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