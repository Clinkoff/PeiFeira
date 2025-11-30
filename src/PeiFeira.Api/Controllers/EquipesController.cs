using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Equipes;
using PeiFeira.Communication.Requests.Equipes;
using PeiFeira.Communication.Responses.Equipes;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EquipesController : ControllerBase
{
    private readonly EquipeAppService _equipeAppService;

    public EquipesController(EquipeAppService equipeAppService)
    {
        _equipeAppService = equipeAppService;
    }

    [HttpPost]
    [Authorize(Roles = "Aluno")]
    public async Task<ActionResult<EquipeResponse>> Create([FromBody] CreateEquipeRequest request)
    {
        var response = await _equipeAppService.CriarAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipeResponse>> GetById(Guid id)
    {
        var response = await _equipeAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("{id}/detalhes")]
    public async Task<ActionResult<EquipeDetailResponse>> GetByIdWithDetails(Guid id)
    {
        var response = await _equipeAppService.BuscarPorIdComDetalhesAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipeResponse>>> GetAll()
    {
        var response = await _equipeAppService.ListarTodasAsync();
        return Ok(response);
    }

    [HttpGet("ativas")]
    public async Task<ActionResult<IEnumerable<EquipeResponse>>> GetActive()
    {
        var response = await _equipeAppService.ListarAtivasAsync();
        return Ok(response);
    }

    [HttpGet("lider/{liderId}")]
    public async Task<ActionResult<EquipeResponse>> GetByLiderId(Guid liderId)
    {
        var response = await _equipeAppService.BuscarPorLiderAsync(liderId);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("codigo/{codigo}")]
    public async Task<ActionResult<EquipeResponse>> GetByCodigo(string codigo)
    {
        var response = await _equipeAppService.BuscarPorCodigoAsync(codigo);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EquipeResponse>> Update(Guid id, [FromBody] UpdateEquipeRequest request)
    {
        var response = await _equipeAppService.AtualizarAsync(id, request);
        return Ok(response);
    }

    [HttpPut("{id}/regenerar-codigo")]
    public async Task<ActionResult<EquipeResponse>> RegenerarCodigo(Guid id)
    {
        var response = await _equipeAppService.RegenerarCodigoAsync(id);
        return Ok(response);
    }

    [HttpGet("com-projeto")]
    public async Task<ActionResult<IEnumerable<EquipeResponse>>> GetComProjeto()
    {
        var response = await _equipeAppService.ListarComProjetoAsync();
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _equipeAppService.ExcluirAsync(id);
        return result ? NoContent() : NotFound();
    }
}
