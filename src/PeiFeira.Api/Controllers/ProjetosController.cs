using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Projetos;
using PeiFeira.Communication.Requests.Projetos;
using PeiFeira.Communication.Responses.Projetos;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjetosController : ControllerBase
{
    private readonly ProjetoAppService _projetoAppService;

    public ProjetosController(ProjetoAppService projetoAppService)
    {
        _projetoAppService = projetoAppService;
    }

    [HttpPost]
    [Authorize(Roles = "Aluno")]
    public async Task<ActionResult<ProjetoResponse>> Create([FromBody] CreateProjetoRequest request)
    {
        var response = await _projetoAppService.CriarAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjetoResponse>> GetById(Guid id)
    {
        var response = await _projetoAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("{id}/detalhes")]
    public async Task<ActionResult<ProjetoDetailResponse>> GetByIdWithDetails(Guid id)
    {
        var response = await _projetoAppService.BuscarPorIdComDetalhesAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjetoResponse>>> GetAll()
    {
        var response = await _projetoAppService.ListarTodosAsync();
        return Ok(response);
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<ProjetoResponse>>> GetActive()
    {
        var response = await _projetoAppService.ListarAtivosAsync();
        return Ok(response);
    }

    [HttpGet("equipe/{equipeId}")]
    public async Task<ActionResult<ProjetoResponse>> GetByEquipeId(Guid equipeId)
    {
        var response = await _projetoAppService.BuscarPorEquipeAsync(equipeId);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("disciplina/{disciplinaPIId}")]
    public async Task<ActionResult<IEnumerable<ProjetoResponse>>> GetByDisciplinaPIId(Guid disciplinaPIId)
    {
        var response = await _projetoAppService.ListarPorDisciplinaPIAsync(disciplinaPIId);
        return Ok(response);
    }

    [HttpGet("com-empresa")]
    public async Task<ActionResult<IEnumerable<ProjetoResponse>>> GetProjetosComEmpresa()
    {
        var response = await _projetoAppService.ListarProjetosComEmpresaAsync();
        return Ok(response);
    }

    [HttpGet("academicos")]
    public async Task<ActionResult<IEnumerable<ProjetoResponse>>> GetProjetosAcademicos()
    {
        var response = await _projetoAppService.ListarProjetosAcademicosAsync();
        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Aluno,Professor")]
    public async Task<ActionResult<ProjetoResponse>> Update(Guid id, [FromBody] UpdateProjetoRequest request)
    {
        var response = await _projetoAppService.AtualizarAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _projetoAppService.ExcluirAsync(id);
        return result ? NoContent() : NotFound();
    }
}
