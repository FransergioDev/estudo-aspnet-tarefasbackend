using Microsoft.EntityFrameworkCore;
using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories;

public interface ITarefaRepository
{
    List<Tarefa> Read();
    Tarefa? Get(Guid id);
    bool Create(Tarefa tarefa);
    bool Update(Guid id, Tarefa tarefa);
    bool Delete(Guid id);
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

    public bool Create(Tarefa tarefa)
    {
        tarefa.Id = Guid.NewGuid();
        
        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();
        return true;
    }

    public bool Update(Guid id, Tarefa tarefa)
    {
        tarefa.Id = id;
            
        if (tarefa.Id != Guid.Empty) return false;
        
        var updateTarefa = _context.Tarefas.Find(tarefa.Id);
        
        if (updateTarefa == null) return false;
        
        updateTarefa.Nome = tarefa.Nome;
        updateTarefa.Concluida = tarefa.Concluida;
        
        _context.Entry(tarefa).State = EntityState.Modified;
        _context.SaveChanges();
        
        return true;
    }

    public bool Delete(Guid id)
    {
        var tarefa = _context.Tarefas.Find(id);
        
        if (tarefa == null) return false;
        
        _context.Entry(tarefa).State = EntityState.Deleted;
        _context.SaveChanges();
        
        return true;
    }
}