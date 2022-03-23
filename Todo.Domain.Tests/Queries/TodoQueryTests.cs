using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueriesTests;

[TestClass]
public class TodoQueryTests
{
    // Red, Green, Refactory

    // Write all tests signature, make everything fail
    // Make everything pass
    // Refactor   

    public TodoQueryTests()
    {
        _items = new List<TodoItem>();

        _items.Add(new TodoItem("Title 01", "me@gmail.com", DateTime.UtcNow.AddDays(5)));
        _items.Add(new TodoItem("Title 02", "me@gmail.com", DateTime.UtcNow.AddDays(5)));
        _items.Add(new TodoItem("Title 03", "me@gmail.com", DateTime.UtcNow.AddDays(5)));
        _items.Add(new TodoItem("Title 04", "me@gmail.com", DateTime.UtcNow.AddDays(4)));
        _items.Add(new TodoItem("Title 05", "me@gmail.com", DateTime.UtcNow.AddDays(4)));
        _items.Add(new TodoItem("Title 06", "me@gmail.com", DateTime.UtcNow.AddDays(4)));

        _items.Add(new TodoItem("Title 01", "you@gmail.com", DateTime.UtcNow.AddDays(5)));
        _items.Add(new TodoItem("Title 02", "you@gmail.com", DateTime.UtcNow.AddDays(5)));
        _items.Add(new TodoItem("Title 03", "you@gmail.com", DateTime.UtcNow.AddDays(5)));
        _items.Add(new TodoItem("Title 04", "you@gmail.com", DateTime.UtcNow.AddDays(3)));
        _items.Add(new TodoItem("Title 05", "you@gmail.com", DateTime.UtcNow.AddDays(3)));

        _items.Add(new TodoItem("Title 06", "justOne@gmail.com", DateTime.UtcNow.AddDays(3)));

        var todoDone = new TodoItem("Title 07", "done@gmail.com", DateTime.UtcNow.AddDays(3));
        todoDone.SetAsDone();
        _items.Add(todoDone);
    }

    private List<TodoItem> _items;

    [TestMethod]
    public void Given_a_query_should_return_just_todos_from_informed_user()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetAll("justOne@gmail.com"));
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    public void Given_a_query_should_return_just_todos_undone_from_informed_user()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetAllUndone("you@gmail.com"));
        Assert.AreEqual(5, result.Count());
    }

    [TestMethod]
    public void Given_a_query_should_return_just_todos_done_from_informed_user()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetAllDone("done@gmail.com"));
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    public void Given_a_query_should_return_just_todos_undone_from_informed_date_and_user()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetByDate("you@gmail.com", DateTime.UtcNow.AddDays(5), false));
        Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void Given_a_query_should_return_just_todos_done_from_informed_date_and_user()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetByDate("you@gmail.com", DateTime.UtcNow.AddDays(5), true));
        Assert.AreEqual(0, result.Count());
    }

}