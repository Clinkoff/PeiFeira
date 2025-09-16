namespace PeiFeira.Domain.Bases.Extensions;

public static class AuditableExtensions
{
    public static void MarcarComoAlterado(this Auditable entidade)
    {
        entidade.AlteradoEm = DateTime.UtcNow;
    }

    public static void MarcarComoDeletado(this Auditable entidade)
    {
        entidade.DeletadoEm = DateTime.UtcNow;

        if (entidade is IBaseEntity baseEntity)
        {
            baseEntity.IsActive = false;
        }
    }

    public static void DefinirDataCriacao(this Auditable entidade)
    {
        entidade.CriadoEm = DateTime.UtcNow;
    }

    public static bool FoiDeletado(this Auditable entidade)
    {
        return entidade.DeletadoEm.HasValue;
    }

    public static bool FoiAlterado(this Auditable entidade)
    {
        return entidade.AlteradoEm.HasValue;
    }
}
