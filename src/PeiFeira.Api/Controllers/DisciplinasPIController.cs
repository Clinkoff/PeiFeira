using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.DisciplinasPI;
using PeiFeira.Communication.Requests.DisciplinaPI;
using PeiFeira.Communication.Responses.DisciplinaPI;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisciplinasPIController : ControllerBase
{
    private readonly DisciplinaPIAppService _disciplinaPIAppService;

    public DisciplinasPIController(DisciplinaPIAppService disciplinaPIAppService)
    {
        _disciplinaPIAppService = disciplinaPIAppService;
    }

    [HttpPost]
    // [Authorize(Roles = "Professor,Coordenador")]
    public async Task<ActionResult<DisciplinaPIResponse>> Create([FromBody] CreateDisciplinaPIRequest request)
    {
        var response = await _disciplinaPIAppService.CriarAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DisciplinaPIResponse>> GetById(Guid id)
    {
        var response = await _disciplinaPIAppService.BuscarPorIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }


    [HttpGet("{id}/detalhes")]
    public async Task<ActionResult<DisciplinaPIDetailResponse>> GetByIdWithDetails(Guid id)
    {
        var response = await _disciplinaPIAppService.BuscarPorIdComDetalhesAsync(id);
        return response != null ? Ok(response) : NotFound();
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<DisciplinaPIResponse>>> GetAll()
    {
        var response = await _disciplinaPIAppService.ListarTodasAsync();
        return Ok(response);
    }


    [HttpGet("ativas")]
    public async Task<ActionResult<IEnumerable<DisciplinaPIResponse>>> GetActive()
    {
        var response = await _disciplinaPIAppService.ListarAtivasAsync();
        return Ok(response);
    }

    [HttpGet("semestre/{semestreId}")]
    public async Task<ActionResult<IEnumerable<DisciplinaPIResponse>>> GetBySemestre(Guid semestreId)
    {
        var response = await _disciplinaPIAppService.ListarPorSemestreAsync(semestreId);
        return Ok(response);
    }


    [HttpGet("professor/{perfilProfessorId}")]
    public async Task<ActionResult<IEnumerable<DisciplinaPIResponse>>> GetByProfessor(Guid perfilProfessorId)
    {
        var response = await _disciplinaPIAppService.ListarPorProfessorAsync(perfilProfessorId);
        return Ok(response);
    }


    [HttpGet("turma/{turmaId}")]
    public async Task<ActionResult<IEnumerable<DisciplinaPIResponse>>> GetByTurma(Guid turmaId)
    {
        var response = await _disciplinaPIAppService.ListarPorTurmaAsync(turmaId);
        return Ok(response);
    }

    [HttpPut("{id}")]
    // [Authorize(Roles = "Professor,Coordenador")]
    public async Task<ActionResult<DisciplinaPIResponse>> Update(Guid id, [FromBody] UpdateDisciplinaPIRequest request)
    {
        var response = await _disciplinaPIAppService.AtualizarAsync(id, request);
        return Ok(response);
    }


    [HttpDelete("{id}")]
    // [Authorize(Roles = "Professor,Coordenador")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _disciplinaPIAppService.ExcluirAsync(id);
        return result ? NoContent() : NotFound();
    }

    [HttpPost("{disciplinaPIId}/turmas/{turmaId}")]
    // [Authorize(Roles = "Professor,Coordenador")]
    public async Task<ActionResult> AssociarTurma(Guid disciplinaPIId, Guid turmaId)
    {
        var result = await _disciplinaPIAppService.AssociarTurmaAsync(disciplinaPIId, turmaId);
        return result ? Ok("Turma associada com sucesso") : BadRequest("Turma já está associada ou não existe");
    }

    [HttpDelete("{disciplinaPIId}/turmas/{turmaId}")]
    // [Authorize(Roles = "Professor,Coordenador")]
    public async Task<ActionResult> RemoverTurma(Guid disciplinaPIId, Guid turmaId)
    {
        var result = await _disciplinaPIAppService.RemoverTurmaAsync(disciplinaPIId, turmaId);
        return result ? Ok("Turma removida com sucesso") : NotFound();
    }
}
