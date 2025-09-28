using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class PerfilAlunoRepository : BaseRepository<PerfilAluno>, IPerfilAlunoRepository
{
    public PerfilAlunoRepository(PeiFeiraDbContext context) : base(context)
    {

    }
}
