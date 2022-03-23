using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface ITodoRepository
{
    bool Create(TodoItem todo);
    bool Update(TodoItem todo);
    bool Delete(TodoItem todo);
    TodoItem? GetById(Guid id, string user);
    IEnumerable<TodoItem> GetAll(string user);
    IEnumerable<TodoItem> GetAllDone(string user);
    IEnumerable<TodoItem> GetAllUndone(string user);
    IEnumerable<TodoItem> GetByDate(string user, DateTime executionDate, bool done);
}
