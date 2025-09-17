using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Communication.Responses.Usuario;

public class ConviteResponse
{
    public Guid Id { get; set; }
    public Guid EquipeId { get; set; }
    public string NomeEquipe { get; set; } = string.Empty;
    public Guid ConvidadoPorId { get; set; }
    public string NomeConvidadoPor { get; set; } = string.Empty;  // Nome do líder
    public Guid ConvidadoId { get; set; }
    public string NomeConvidado { get; set; } = string.Empty;     // Nome do convidado
    public string? Mensagem { get; set; }
    public string Status { get; set; } = string.Empty;           // "Pendente", "Aceito", etc.
    public string? MotivoResposta { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? RespondidoEm { get; set; }
}