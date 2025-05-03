using Microsoft.AspNetCore.Mvc;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers;

[ApiController]
[Route("api/tarefas")]
public class TarefaController : ControllerBase
{

    [HttpGet]
    public IActionResult Read([FromServices] ITarefaRepository repository)
    {
        var tarefas = repository.Read();
        return Ok(tarefas);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Tarefa tarefa, [FromServices] ITarefaRepository repository)
    {
        if (!ModelState.IsValid) return BadRequest();

        repository.Create(tarefa);
        return Created();
    }
}