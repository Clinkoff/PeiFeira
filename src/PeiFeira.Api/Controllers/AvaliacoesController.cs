using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Avaliacoes;
using PeiFeira.Communication.Requests.Avaliacoes;
using PeiFeira.Communication.Responses.Avaliacoes;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AvaliacoesController : ControllerBase
{
    private readonly AvaliacaoAppService _avaliacaoAppService;

    public AvaliacoesController(AvaliacaoAppService avaliacaoAppService)
    {
        _avaliacaoAppService = avaliacaoAppService;
    }

    [HttpPost]
    [Authorize(Roles = "Professor")]
    public async Task<ActionResult<AvaliacaoResponse>> Create([FromBody] CreateAvaliacaoRequest request)
    {
        var response = await _avaliacaoAppService.CriarAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AvaliacaoResponse>> GetById(Guid id)
    {
        var response = await _avaliacaoAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AvaliacaoResponse>>> GetAll()
    {
        var response = await _avaliacaoAppService.ListarTodasAsync();
        return Ok(response);
    }

    [HttpGet("equipe/{equipeId}")]
    public async Task<ActionResult<IEnumerable<AvaliacaoResponse>>> GetByEquipeId(Guid equipeId)
    {
        var response = await _avaliacaoAppService.ListarPorEquipeAsync(equipeId);
        return Ok(response);
    }

    [HttpGet("avaliador/{avaliadorId}")]
    public async Task<ActionResult<IEnumerable<AvaliacaoResponse>>> GetByAvaliadorId(Guid avaliadorId)
    {
        var response = await _avaliacaoAppService.ListarPorAvaliadorAsync(avaliadorId);
        return Ok(response);
    }

    [HttpGet("equipe/{equipeId}/media")]
    public async Task<ActionResult<decimal>> GetMediaEquipe(Guid equipeId)
    {
        var media = await _avaliacaoAppService.ObterMediaEquipeAsync(equipeId);
        return Ok(new { equipeId, media });
    }

    [HttpGet("media-geral")]
    public async Task<ActionResult<decimal>> GetMediaGeral()
    {
        var media = await _avaliacaoAppService.ObterMediaGeralAsync();
        return Ok(new { media });
    }

    [HttpGet("faixa-nota")]
    public async Task<ActionResult<IEnumerable<AvaliacaoResponse>>> GetByFaixaNota(
        [FromQuery] decimal min = 0,
        [FromQuery] decimal max = 10)
    {
        var response = await _avaliacaoAppService.ListarPorFaixaNotaAsync(min, max);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Professor")]
    public async Task<ActionResult<AvaliacaoResponse>> Update(Guid id, [FromBody] UpdateAvaliacaoRequest request)
    {
        var response = await _avaliacaoAppService.AtualizarAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _avaliacaoAppService.ExcluirAsync(id);
        return result ? NoContent() : NotFound();
    }
}
