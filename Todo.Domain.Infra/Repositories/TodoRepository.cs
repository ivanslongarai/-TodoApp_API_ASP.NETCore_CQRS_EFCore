using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Repositories;

public class TodoRepository : ITodoRepository
{

    private readonly DataContext _ctx;

    public TodoRepository(DataContext ctx)
    {
        _ctx = ctx;
    }

    public bool Create(TodoItem todo)
    {
        try
        {
            _ctx.Todos!.Add(todo);
            _ctx.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<TodoItem> GetAll(string user)
    {
        return _ctx.Todos!
            .AsNoTracking()
            .Where(TodoQueries.GetAll(user))
            .OrderBy(x => x.CreatedAt);
    }

    public IEnumerable<TodoItem> GetAllDone(string user)
    {
        return _ctx.Todos!
            .AsNoTracking()
            .Where(TodoQueries.GetAllDone(user))
            .OrderBy(x => x.CreatedAt);
    }

    public IEnumerable<TodoItem> GetAllUndone(string user)
    {
        return _ctx.Todos!
            .AsNoTracking()
            .Where(TodoQueries.GetAllUndone(user))
            .OrderBy(x => x.CreatedAt);
    }

    public IEnumerable<TodoItem> GetByDate(string user, DateTime executionDate, bool done)
    {
        return _ctx.Todos!
            .AsNoTracking()
            .Where(TodoQueries.GetByDate(user, executionDate, done))
            .OrderBy(x => x.CreatedAt);
    }

    public TodoItem? GetById(Guid id, string user)
    {
        return _ctx.Todos!
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id && x.User == user);
    }

    public bool Update(TodoItem todo)
    {
        try
        {
            _ctx.Entry(todo).State = EntityState.Modified;
            _ctx.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(TodoItem todo)
    {
        try
        {
            _ctx.Todos!.Remove(todo);
            _ctx.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}