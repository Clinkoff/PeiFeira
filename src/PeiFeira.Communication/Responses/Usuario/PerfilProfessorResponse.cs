using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Usuario;

public class PerfilProfessorResponse 
{
    public Guid Id { get; set; }
    public string? Departamento { get; set; }
    public string? Titulacao { get; set; }
    public string? AreaEspecializacao { get; set; }
}
