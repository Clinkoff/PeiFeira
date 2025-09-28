namespace PeiFeira.Domain.Interfaces;
// Claudinho pediu para criar um lugar a onde todos os repositórios fiquem acessíveis, 
// para que o serviço de aplicação possa usar todos os repositórios através de uma única instância de UnitOfWork.
public interface IUnitOfWork : IDisposable
{
    // REPOSITÓRIOS PRINCIPAIS
    IUsuarioRepository Usuarios { get; }
    IEquipeRepository Equipes { get; }
    IProjetoRepository Projetos { get; }
    IAvaliacaoRepository Avaliacoes { get; }

    // REPOSITÓRIOS DE RELACIONAMENTO
    IMembroEquipeRepository MembrosEquipe { get; }
    IConviteEquipeRepository ConvitesEquipe { get; }

    // REPOSITÓRIOS DE PERFIL
    IPerfilAlunoRepository PerfisAluno { get; }
    IPerfilProfessorRepository PerfisProfessor { get; }

    // CONTROLE DE TRANSAÇÕES
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
