using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Communication.Responses.Base;

public abstract class BaseResponse
{
    public DateTime CriadoEm { get; set; }
    public DateTime? AlteradoEm { get; set; }
}
