namespace PeiFeira.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> MensagemErros { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ResponseErrorJson(string errorMessage, int statusCode = 400)
    {
        MensagemErros = [errorMessage];
        StatusCode = statusCode;
        Message = "Erro na requisição";
    }

    public ResponseErrorJson(List<string> errorMessages, int statusCode = 400)
    {
        MensagemErros = errorMessages;
        StatusCode = statusCode;
        Message = "Erros na requisição";
    }
}