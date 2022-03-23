using System;
using System.Collections.Generic;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.Tests.FakeRepositories;

public class FakeTodoRepository : ITodoRepository
{
    public bool Create(TodoItem todo)
    {
        return true;
    }

    public bool Delete(TodoItem todo)
    {
        return true;
    }

    public IEnumerable<TodoItem> GetAll(string user)
    {
        return new List<TodoItem>();
    }

    public IEnumerable<TodoItem> GetAllDone(string user)
    {
        return new List<TodoItem>();
    }

    public IEnumerable<TodoItem> GetAllUndone(string user)
    {
        return new List<TodoItem>();
    }

    public IEnumerable<TodoItem> GetByDate(string user, DateTime executionDate, bool done)
    {
        return new List<TodoItem>();
    }

    public TodoItem? GetById(Guid id, string user)
    {
        return null;
    }

    public bool Update(TodoItem todo)
    {
        return true;
    }
}