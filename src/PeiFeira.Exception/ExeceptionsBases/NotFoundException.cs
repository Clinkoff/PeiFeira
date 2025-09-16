namespace PeiFeira.Exception.ExeceptionsBases;

public class NotFoundException : BaseException
{
    public NotFoundException(string entity, object id)
        : base($"{entity} com ID {id} não foi encontrado", "NOT_FOUND", 404)
    {
    }

    public NotFoundException(string message)
        : base(message, "NOT_FOUND", 404)
    {
    }
}