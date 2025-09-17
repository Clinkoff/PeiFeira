using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Communication.Requests.Equipe;

public class InviteUsuarioRequest
{
    public Guid UsuarioId { get; set; }
    public string? Mensagem { get; set; }
}
