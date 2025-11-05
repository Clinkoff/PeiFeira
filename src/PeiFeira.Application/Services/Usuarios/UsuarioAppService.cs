using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.Auth;
using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Communication.Responses.Usuario;

namespace PeiFeira.Application.Services.Usuarios;

public class UsuarioAppService
{
    private readonly IUsuarioManager _usuarioManager;
    private readonly ILogger<UsuarioAppService> _logger;

    public UsuarioAppService(IUsuarioManager usuarioManager, ILogger<UsuarioAppService> logger)
    {
        _usuarioManager = usuarioManager;
        _logger = logger;
    }

    public async Task<UsuarioResponse> CriarAsync(CreateUsuarioRequest request)
    {
        _logger.LogInformation("Iniciando criação de usuário para matrícula: {Matricula}", request.Matricula);

        var response = await _usuarioManager.CreateAsync(request);
        _logger.LogInformation("Usuário criado com sucesso. ID: {Id}", response.Id);
        return response;
    }

    public async Task<UsuarioResponse> AtualizarAsync(Guid id, UpdateUsuarioRequest request)
    {
        _logger.LogInformation("Iniciando atualização de usuário. ID: {Id}", id);

        var response = await _usuarioManager.UpdateAsync(id, request);
        _logger.LogInformation("Usuário atualizado com sucesso. ID: {Id}", id);
        return response;
    }

    public async Task<bool> ExcluirAsync(Guid id)
    {
        _logger.LogInformation("Iniciando exclusão de usuário. ID: {Id}", id);

        var result = await _usuarioManager.DeleteAsync(id);
        _logger.LogInformation("Usuário excluído com sucesso. ID: {Id}", id);
        return result;
    }

    public async Task<UsuarioResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando usuário por ID: {Id}", id);
        return await _usuarioManager.GetByIdAsync(id);
    }

    public async Task<UsuarioResponse?> BuscarPorMatriculaAsync(string matricula)
    {
        _logger.LogInformation("Buscando usuário por matrícula: {Matricula}", matricula);
        return await _usuarioManager.GetByMatriculaAsync(matricula);
    }

    public async Task<UsuarioResponse?> BuscarPorEmailAsync(string email)
    {
        _logger.LogInformation("Buscando usuário por email: {Email}", email);
        return await _usuarioManager.GetByEmailAsync(email);
    }

    public async Task<IEnumerable<UsuarioResponse>> ListarTodosAsync()
    {
        _logger.LogInformation("Listando todos os usuários");
        return await _usuarioManager.GetAllAsync();
    }

    public async Task<IEnumerable<UsuarioResponse>> ListarAtivosAsync()
    {
        _logger.LogInformation("Listando usuários ativos");
        return await _usuarioManager.GetActiveAsync();
    }

    public async Task<IEnumerable<UsuarioResponse>> ListarProfessoresAsync()
    {
        _logger.LogInformation("Listando professores");
        return await _usuarioManager.GetProfessoresAsync();
    }

    public async Task<IEnumerable<UsuarioResponse>> ListarAlunosAsync()
    {
        _logger.LogInformation("Listando alunos");
        return await _usuarioManager.GetAlunosAsync();
    }

    public async Task<UsuarioResponse?> LoginAsync(LoginRequest request)
    {
        _logger.LogInformation("Tentativa de login para matrícula: {Matricula}", request.Matricula);

        var response = await _usuarioManager.LoginAsync(request);
        if (response != null)
        {
            _logger.LogInformation("Login realizado com sucesso. Usuário: {Id}", response.Id);
        }
        else
        {
            _logger.LogWarning("Falha no login para matrícula: {Matricula}", request.Matricula);
        }
        return response;
    }

    public async Task<bool> MudarSenhaAsync(Guid id, MudarSenhaRequest request)
    {
        _logger.LogInformation("Iniciando mudança de senha para usuário: {Id}", id);

        var result = await _usuarioManager.MudarSenhaAsync(id, request);
        _logger.LogInformation("Senha alterada com sucesso para usuário: {Id}", id);
        return result;
    }

    public async Task<bool> ExisteMatriculaAsync(string matricula)
    {
        return await _usuarioManager.ExistsByMatriculaAsync(matricula);
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _usuarioManager.ExistsByEmailAsync(email);
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAlunosDisponiveisAsync(Guid turmaId)
    {
        return await _usuarioManager.GetAlunosDisponiveisAsync(turmaId);
    }
}
