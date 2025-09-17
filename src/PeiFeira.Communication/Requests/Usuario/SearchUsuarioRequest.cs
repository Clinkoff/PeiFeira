namespace PeiFeira.Communication.Requests.Usuario;

public class SearchUsuarioRequest
{
    public string? Nome { get; set; }
    public string? Matricula { get; set; }
    public string? Email { get; set; }
    public int? Role { get; set; }

    public int Page { get; set; } 
    public int PageSize { get; set; } 
}
