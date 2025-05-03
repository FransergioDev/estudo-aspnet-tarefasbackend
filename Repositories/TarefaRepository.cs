using Microsoft.EntityFrameworkCore;
using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories;

public interface ITarefaRepository
{
    List<Tarefa> Read();
    Tarefa? Get(Guid id);
    void Create(Tarefa tarefa);
    void Update(Tarefa tarefa);
    void Delete(Guid id);
}

public class TarefaRepository: ITarefaRepository
{
    private readonly DataContext _context;

    public TarefaRepository(DataContext context)
    {
        _context = context;
    }
    
    public List<Tarefa> Read()
    {
       return _context.Tarefas.ToList();
    }

    public Tarefa? Get(Guid id)
    {
        return _context.Tarefas.Find(id);
    }

    public void Create(Tarefa tarefa)
    {
        tarefa.Id = Guid.NewGuid();
        
        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();
    }

    public void Update(Tarefa tarefa)
    {
        if (tarefa.Id != Guid.Empty) return;
        
        var updateTarefa = _context.Tarefas.Find(tarefa.Id);
        
        if (updateTarefa == null) return;
        
        updateTarefa.Nome = tarefa.Nome;
        updateTarefa.Concluida = tarefa.Concluida;
        
        _context.Entry(tarefa).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var tarefa = _context.Tarefas.Find(id);
        
        if (tarefa == null) return;
        
        _context.Entry(tarefa).State = EntityState.Deleted;
        _context.SaveChanges();
    }
}