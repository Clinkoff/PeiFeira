using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Domain.Interfaces;

public interface IConviteEquipeRepository : IBaseRepository<ConviteEquipe>
{
    Task<IEnumerable<ConviteEquipe>> GetByEquipeIdAsync(Guid equipeId);
    Task<IEnumerable<ConviteEquipe>> GetByConvidadoIdAsync(Guid convidadoId);
    Task<ConviteEquipe?> GetByEquipeAndUsuarioAsync(Guid equipeId, Guid usuarioId);

    Task<IEnumerable<ConviteEquipe>> GetPendentesAsync(Guid usuarioId);
    Task<IEnumerable<ConviteEquipe>> GetByStatusAsync(StatusConvite status);

    Task<bool> JaFoiConvidadoAsync(Guid equipeId, Guid usuarioId);
    Task<bool> TemConvitePendenteAsync(Guid equipeId, Guid usuarioId);

    Task<bool> CancelarConvitesEquipeAsync(Guid equipeId);
    Task<int> CountConvitesPendentesAsync(Guid usuarioId);
}