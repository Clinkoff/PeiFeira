using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Exception.ExeceptionsBases;

public class ValidationException : BaseException
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors)
        : base("Dados inválidos", "VALIDATION_ERROR", 400)
    {
        Errors = errors;
    }

    public ValidationException(string field, string message)
        : base($"Erro de validação no campo {field}", "VALIDATION_ERROR", 400)
    {
        Errors = new Dictionary<string, string[]>
        {
            { field, new[] { message } }
        };
    }
}