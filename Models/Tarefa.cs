namespace TarefasBackEnd.Models;

public class Tarefa
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public required string Nome { get; set; }
    public bool Concluida { get; set; }
}