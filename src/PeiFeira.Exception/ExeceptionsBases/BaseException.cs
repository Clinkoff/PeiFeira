namespace PeiFeira.Exception.ExeceptionsBases;

public abstract class BaseException : SystemException
{
    public string ErrorCode { get; }
    public int StatusCode { get; }

    protected BaseException(string message, string errorCode, int statusCode) : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }

    protected BaseException(string message, string errorCode, int statusCode, System.Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}
