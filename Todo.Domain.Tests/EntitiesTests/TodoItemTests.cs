using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;

namespace Todo.Domain.Tests.EntitiesTests;

[TestClass]
public class TodoItemTests
{
    // Red, Green, Refactory

    // Write all tests signature, make everything fail
    // Make everything pass
    // Refactor    

    [TestMethod]
    public void Given_a_valid_Todo_should_return_done_false()
    {
        var todo = new TodoItem(
            "Title",
            "me@gmail.com",
            DateTime.UtcNow.AddDays(5)
       );
        Assert.AreEqual(todo.Done, false);
    }
}