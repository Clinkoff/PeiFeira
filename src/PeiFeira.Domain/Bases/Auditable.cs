namespace PeiFeira.Domain.Bases;

public abstract class Auditable
{
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? AlteradoEm { get; set; }
    public DateTime? DeletadoEm { get; set; }
}