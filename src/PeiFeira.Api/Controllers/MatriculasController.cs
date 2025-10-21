using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Matriculas;
using PeiFeira.Communication.Requests.Matriculas;
using PeiFeira.Communication.Responses.Matriculas;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculasController : ControllerBase
{
    private readonly MatriculaAppService _matriculaAppService;

    public MatriculasController(MatriculaAppService matriculaAppService)
    {
        _matriculaAppService = matriculaAppService;
    }
    [HttpPost]
    public async Task<ActionResult<MatriculaResponse>> MatricularAluno([FromBody] CreateMatriculaRequest request)
    {
        var response = await _matriculaAppService.MatricularAlunoAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MatriculaResponse>> GetById(Guid id)
    {
        var response = await _matriculaAppService.GetByIdAsync(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet("turma/{turmaId}")]
    public async Task<ActionResult<IEnumerable<MatriculaResponse>>> GetByTurma(Guid turmaId)
    {
        var response = await _matriculaAppService.GetByTurmaIdAsync(turmaId);
        return Ok(response);
    }

    [HttpGet("aluno/{perfilAlunoId}")]
    public async Task<ActionResult<IEnumerable<MatriculaResponse>>> GetByAluno(Guid perfilAlunoId)
    {
        var response = await _matriculaAppService.GetByAlunoIdAsync(perfilAlunoId);
        return Ok(response);
    }

    [HttpGet("aluno/{perfilAlunoId}/atual")]
    public async Task<ActionResult<MatriculaResponse>> GetMatriculaAtual(Guid perfilAlunoId)
    {
        var response = await _matriculaAppService.GetMatriculaAtualByAlunoAsync(perfilAlunoId);
        return response != null ? Ok(response) : NotFound(new { message = "Aluno não possui matrícula ativa" });
    }

    [HttpPut("transferir")]
    public async Task<ActionResult> TransferirAluno([FromBody] TransferirAlunoRequest request)
    {
        var result = await _matriculaAppService.TransferirAlunoAsync(request.PerfilAlunoId, request.NovaTurmaId);
        return result ? Ok(new { message = "Aluno transferido com sucesso" }) : BadRequest();
    }

    [HttpDelete("{matriculaId}")]
    public async Task<ActionResult> Desmatricular(Guid matriculaId)
    {
        var result = await _matriculaAppService.DesmatricularAlunoAsync(matriculaId);
        return result ? NoContent() : NotFound();
    }
}