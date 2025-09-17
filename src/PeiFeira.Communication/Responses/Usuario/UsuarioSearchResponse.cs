using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Communication.Responses.Usuario;

public class UsuarioSearchResponse
{
    public Guid Id { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public bool JaEstaEmEquipe { get; set; }
    public string? NomeEquipeAtual { get; set; }
    public bool JaFoiConvidado { get; set; }  // Para esta equipe específica
}
