using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Turmas;
using PeiFeira.Communication.Requests.Turma;
using PeiFeira.Communication.Responses.Turmas;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurmasController : ControllerBase
{
    private readonly TurmaAppService _turmaAppService;

    public TurmasController(TurmaAppService turmaAppService)
    {
        _turmaAppService = turmaAppService;
    }

    [HttpPost]
    public async Task<ActionResult<TurmaResponse>> Create([FromBody] CreateTurmaRequest request)
    {
        var response = await _turmaAppService.CriarAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TurmaResponse>> GetById(Guid id)
    {
        var response = await _turmaAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TurmaResponse>>> GetAll()
    {
        var response = await _turmaAppService.ListarTodosAsync();
        return Ok(response);
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<TurmaResponse>>> GetActive()
    {
        var response = await _turmaAppService.ListarAtivosAsync();
        return Ok(response);
    }

    [HttpGet("semestre/{semestreId}")]
    public async Task<ActionResult<IEnumerable<TurmaResponse>>> GetBySemestreId(Guid semestreId)
    {
        var response = await _turmaAppService.ListarPorSemestreIdAsync(semestreId);
        return Ok(response);
    }

    [HttpGet("curso/{curso}")]
    public async Task<ActionResult<IEnumerable<TurmaResponse>>> GetByCurso(string curso)
    {
        var response = await _turmaAppService.ListarPorCursoAsync(curso);
        return Ok(response);
    }

    [HttpGet("codigo/{codigo}")]
    public async Task<ActionResult<TurmaResponse>> GetByCodigo(string codigo)
    {
        var response = await _turmaAppService.BuscarPorCodigoAsync(codigo);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TurmaResponse>> Update(Guid id, [FromBody] UpdateTurmaRequest request)
    {
        var response = await _turmaAppService.AtualizarAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _turmaAppService.ExcluirAsync(id);
        return result ? NoContent() : NotFound();
    }
}
