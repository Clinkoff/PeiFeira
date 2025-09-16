namespace PeiFeira.Exception.ExeceptionsBases;

public class ConflictException : BaseException
{
    public ConflictException(string message)
        : base(message, "CONFLICT", 409)
    {
    }
}
