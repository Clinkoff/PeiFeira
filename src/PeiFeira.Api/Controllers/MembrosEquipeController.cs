using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.MembrosEquipes;
using PeiFeira.Communication.Requests.MembroEquipe;
using PeiFeira.Communication.Responses.MembroEquipe;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/membros-equipe")]
[Authorize]
public class MembrosEquipeController : ControllerBase
{
    private readonly MembroEquipeAppService _membroEquipeAppService;

    public MembrosEquipeController(MembroEquipeAppService membroEquipeAppService)
    {
        _membroEquipeAppService = membroEquipeAppService;
    }

    [HttpPost]
    public async Task<ActionResult<MembroEquipeResponse>> AddMembro([FromBody] AddMembroEquipeRequest request)
    {
        var response = await _membroEquipeAppService.AdicionarMembroAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpDelete("equipe/{equipeId}/aluno/{perfilAlunoId}")]
    public async Task<ActionResult> RemoveMembro(Guid equipeId, Guid perfilAlunoId)
    {
        var result = await _membroEquipeAppService.RemoverMembroAsync(equipeId, perfilAlunoId);
        return result ? NoContent() : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MembroEquipeResponse>> GetById(Guid id)
    {
        var response = await _membroEquipeAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("equipe/{equipeId}")]
    public async Task<ActionResult<IEnumerable<MembroEquipeResponse>>> GetByEquipeId(Guid equipeId)
    {
        var response = await _membroEquipeAppService.ListarMembrosPorEquipeAsync(equipeId);
        return Ok(response);
    }

    [HttpGet("aluno/{perfilAlunoId}")]
    public async Task<ActionResult<IEnumerable<MembroEquipeResponse>>> GetByPerfilAlunoId(Guid perfilAlunoId)
    {
        var response = await _membroEquipeAppService.ListarEquipesPorAlunoAsync(perfilAlunoId);
        return Ok(response);
    }

    [HttpGet("verificar/{equipeId}/{perfilAlunoId}")]
    public async Task<ActionResult<bool>> VerificarSeMembro(Guid equipeId, Guid perfilAlunoId)
    {
        var isMembro = await _membroEquipeAppService.VerificarSeMembroAsync(equipeId, perfilAlunoId);
        return Ok(isMembro);
    }
}
