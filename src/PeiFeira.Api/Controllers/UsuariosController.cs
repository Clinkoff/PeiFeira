using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Usuarios;
using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Communication.Responses.Usuario;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioAppService _usuarioAppService;

    public UsuariosController(UsuarioAppService usuarioAppService)
    {
        _usuarioAppService = usuarioAppService;
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponse>> Create([FromBody] CreateUsuarioRequest request)
    {
        var response = await _usuarioAppService.CriarAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponse>> GetById(Guid id)
    {
        var response = await _usuarioAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("matricula/{matricula}")]
    public async Task<ActionResult<UsuarioResponse>> GetByMatricula(string matricula)
    {
        var response = await _usuarioAppService.BuscarPorMatriculaAsync(matricula);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UsuarioResponse>> GetByEmail(string email)
    {
        var response = await _usuarioAppService.BuscarPorEmailAsync(email);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
    {
        var response = await _usuarioAppService.ListarTodosAsync();
        return Ok(response);
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetActive()
    {
        var response = await _usuarioAppService.ListarAtivosAsync();
        return Ok(response);
    }

    [HttpGet("professores")]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetProfessores()
    {
        var response = await _usuarioAppService.ListarProfessoresAsync();
        return Ok(response);
    }

    [HttpGet("alunos")]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAlunos()
    {
        var response = await _usuarioAppService.ListarAlunosAsync();
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UsuarioResponse>> Update(Guid id, [FromBody] UpdateUsuarioRequest request)
    {
        var response = await _usuarioAppService.AtualizarAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _usuarioAppService.ExcluirAsync(id);
        return result ? NoContent() : NotFound();
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioResponse>> Login([FromBody] LoginRequest request)
    {
        var response = await _usuarioAppService.LoginAsync(request);
        return response != null ? Ok(response) : Unauthorized("Credenciais inválidas");
    }

    [HttpPut("{id}/mudar-senha")]
    public async Task<ActionResult> MudarSenha(Guid id, [FromBody] MudarSenhaRequest request)
    {
        var result = await _usuarioAppService.MudarSenhaAsync(id, request);
        return result ? Ok("Senha alterada com sucesso") : NotFound();
    }

    [HttpGet("exists/matricula/{matricula}")]
    public async Task<ActionResult<bool>> ExistsByMatricula(string matricula)
    {
        var exists = await _usuarioAppService.ExisteMatriculaAsync(matricula);
        return Ok(exists);
    }

    [HttpGet("exists/email/{email}")]
    public async Task<ActionResult<bool>> ExistsByEmail(string email)
    {
        var exists = await _usuarioAppService.ExisteEmailAsync(email);
        return Ok(exists);
    }
}