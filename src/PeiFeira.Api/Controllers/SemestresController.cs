using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Semestres;
using PeiFeira.Communication.Requests.Semestres;
using PeiFeira.Communication.Responses.Semestres;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SemestresController : ControllerBase
{
    private readonly SemestreAppService _semestreAppService;

    public SemestresController(SemestreAppService semestreAppService)
    {
        _semestreAppService = semestreAppService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<SemestreResponse>> Create([FromBody] CreateSemestreRequest request)
    {
        var response = await _semestreAppService.CriarAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SemestreResponse>> GetById(Guid id)
    {
        var response = await _semestreAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SemestreResponse>>> GetAll()
    {
        var response = await _semestreAppService.ListarTodosAsync();
        return Ok(response);
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<SemestreResponse>>> GetActive()
    {
        var response = await _semestreAppService.ListarAtivosAsync();
        return Ok(response);
    }

    [HttpGet("ano/{ano}")]
    public async Task<ActionResult<IEnumerable<SemestreResponse>>> GetByAno(int ano)
    {
        var response = await _semestreAppService.ListarPorAnoAsync(ano);
        return Ok(response);
    }

    [HttpGet("{ano}/{periodo}")]
    public async Task<ActionResult<SemestreResponse>> GetByAnoAndPeriodo(int ano, int periodo)
    {
        var response = await _semestreAppService.BuscarPorAnoEPeriodoAsync(ano, periodo);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<SemestreResponse>> Update(Guid id, [FromBody] UpdateSemestreRequest request)
    {
        var response = await _semestreAppService.AtualizarAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _semestreAppService.ExcluirAsync(id);
        return result ? NoContent() : NotFound();
    }
}