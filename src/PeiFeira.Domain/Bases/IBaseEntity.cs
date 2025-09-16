namespace PeiFeira.Domain.Bases;

public interface IBaseEntity
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
}