using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.ConviteEquipe;
using PeiFeira.Communication.Requests.ConviteEquipe;
using PeiFeira.Communication.Responses.ConviteEquipe;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/convites-equipe")]
[Authorize]
public class ConvitesEquipeController : ControllerBase
{
    private readonly ConviteEquipeAppService _conviteEquipeAppService;

    public ConvitesEquipeController(ConviteEquipeAppService conviteEquipeAppService)
    {
        _conviteEquipeAppService = conviteEquipeAppService;
    }

    [HttpPost]
    [Authorize(Roles = "Aluno")]
    public async Task<ActionResult<ConviteEquipeResponse>> EnviarConvite([FromBody] CreateConviteEquipeRequest request)
    {
        var response = await _conviteEquipeAppService.EnviarConviteAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}/aceitar")]
    [Authorize(Roles = "Aluno")]
    public async Task<ActionResult<ConviteEquipeResponse>> AceitarConvite(Guid id, [FromBody] RespondConviteRequest request)
    {
        var response = await _conviteEquipeAppService.AceitarConviteAsync(id, request.PerfilAlunoId);
        return Ok(response);
    }

    [HttpPut("{id}/recusar")]
    [Authorize(Roles = "Aluno")]
    public async Task<ActionResult<ConviteEquipeResponse>> RecusarConvite(Guid id, [FromBody] RespondConviteRequest request)
    {
        var response = await _conviteEquipeAppService.RecusarConviteAsync(id, request.PerfilAlunoId);
        return Ok(response);
    }

    [HttpPut("{id}/cancelar")]
    [Authorize(Roles = "Aluno")]
    public async Task<ActionResult<ConviteEquipeResponse>> CancelarConvite(Guid id, [FromBody] RespondConviteRequest request)
    {
        var response = await _conviteEquipeAppService.CancelarConviteAsync(id, request.PerfilAlunoId);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConviteEquipeResponse>> GetById(Guid id)
    {
        var response = await _conviteEquipeAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("pendentes/{perfilAlunoId}")]
    public async Task<ActionResult<IEnumerable<ConviteEquipeResponse>>> GetConvitesPendentes(Guid perfilAlunoId)
    {
        var response = await _conviteEquipeAppService.ListarConvitesPendentesAsync(perfilAlunoId);
        return Ok(response);
    }

    [HttpGet("equipe/{equipeId}")]
    public async Task<ActionResult<IEnumerable<ConviteEquipeResponse>>> GetConvitesByEquipe(Guid equipeId)
    {
        var response = await _conviteEquipeAppService.ListarConvitesPorEquipeAsync(equipeId);
        return Ok(response);
    }
    [HttpGet("count-pendentes/{perfilAlunoId}")]
    public async Task<ActionResult<int>> GetCountPendentes(Guid perfilAlunoId)
    {
        var count = await _conviteEquipeAppService.ContarConvitesPendentesAsync(perfilAlunoId);
        return Ok(count);
    }
}
