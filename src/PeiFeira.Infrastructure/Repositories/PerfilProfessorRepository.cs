using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class PerfilProfessorRepository : BaseRepository<PerfilProfessor>, IPerfilProfessorRepository
{
    public PerfilProfessorRepository(PeiFeiraDbContext context) : base(context)
    {

    }
}
