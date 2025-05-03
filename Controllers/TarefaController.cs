using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/tarefas")]
public class TarefaController : ControllerBase
{

    [HttpGet]
    // [AllowAnonymous]
    public IActionResult Read([FromServices] ITarefaRepository repository)
    {
        string? identity = User.Identity?.Name;
        if (identity == null) return BadRequest();
        var usuarioId = new Guid(identity);
        
        var tarefas = repository.Read(usuarioId);
        return Ok(tarefas);
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(string id, [FromServices] ITarefaRepository repository)
    {
        var tarefaId = Guid.Parse(id);
        var tarefa = repository.Get(tarefaId);
        return Ok(tarefa);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Tarefa tarefa, [FromServices] ITarefaRepository repository)
    {
        if (!ModelState.IsValid) return BadRequest();

        string? identity = User.Identity?.Name;
        if (identity == null) return BadRequest();
        
        tarefa.UsuarioId = new Guid(identity);
        repository.Create(tarefa);
        return Created();
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Tarefa tarefa, [FromServices] ITarefaRepository repository)
    {
        if (!ModelState.IsValid) return BadRequest();

        var tarefaId = Guid.Parse(id);
        var result = repository.Update(tarefaId, tarefa);

        if (!result) return BadRequest();

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Update(string id, [FromServices] ITarefaRepository repository)
    {
        var tarefaId = Guid.Parse(id);
        var result = repository.Delete(tarefaId);

        if (!result) return BadRequest();

        return Ok();
    }
}