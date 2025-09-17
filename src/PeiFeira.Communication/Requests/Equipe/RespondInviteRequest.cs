using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Communication.Requests.Equipe;

public class RespondInviteRequest
{
    public bool Aceitar { get; set; }
    public string? Motivo { get; set; }
}
