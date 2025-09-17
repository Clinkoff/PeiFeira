using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Application.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    public static bool BeValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }

    public static bool BeValidMatricula(string matricula)
    {
        if (string.IsNullOrWhiteSpace(matricula))
            return false;

        return matricula.Length == 10 && matricula.All(char.IsDigit);
    }

    public static bool BeValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var mail = new System.Net.Mail.MailAddress(email);
            return mail.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
