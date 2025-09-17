using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Communication.Requests.Usuario;

public class CreateUsuarioRequest
{
   public string Matricula { get; set; } = string.Empty;
   public string Nome { get; set; } = string.Empty;
   public string Email { get; set; } = string.Empty;
   public string Senha { get; set; } = string.Empty;
   public int Role { get; set; }

}
