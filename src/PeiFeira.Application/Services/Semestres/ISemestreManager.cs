using PeiFeira.Communication.Requests.Semestres;
using PeiFeira.Communication.Responses.Semestres;

namespace PeiFeira.Application.Services.Semestres;

public interface ISemestreManager
{
    Task<SemestreResponse> CreateAsync(CreateSemestreRequest request);
    Task<SemestreResponse> UpdateAsync(Guid id, UpdateSemestreRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<SemestreResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<SemestreResponse>> GetAllAsync();
    Task<IEnumerable<SemestreResponse>> GetActiveAsync();
    Task<IEnumerable<SemestreResponse>> GetByAnoAsync(int ano);
    Task<SemestreResponse?> GetByAnoAndPeriodoAsync(int ano, int periodo);
}