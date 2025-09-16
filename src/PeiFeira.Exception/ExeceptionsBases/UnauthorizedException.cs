using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Exception.ExeceptionsBases;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message = "Acesso não autorizado")
        : base(message, "UNAUTHORIZED", 401)
    {
    }
}
