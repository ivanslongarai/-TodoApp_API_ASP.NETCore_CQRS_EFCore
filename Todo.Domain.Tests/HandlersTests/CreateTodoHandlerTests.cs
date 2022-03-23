using System.Reflection.Metadata;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.FakeRepositories;

namespace Todo.Domain.Tests.HandlersTests;

[TestClass]
public class CreateTodoHandlerTests
{
    // Red, Green, Refactory

    // Write all tests signature, make everything fail
    // Make everything pass
    // Refactor    

    private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand(
                "",
                DateTime.UtcNow.AddDays(5),
                "");

    private readonly CreateTodoCommand _validCommand = new CreateTodoCommand(
            "Title",
            DateTime.UtcNow.AddDays(5),
            "me@gmail.com");


    [TestMethod]
    public void Given_an_invalid_command_should_return_invalid()
    {
        var _handler = GetHandler();
        var result = (GenericCommandResult)_handler.Handle(_invalidCommand);
        Assert.IsFalse(_handler.Valid);
        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public void Given_a_valid_command_should_return_valid()
    {
        var _handler = GetHandler();
        var result = (GenericCommandResult)_handler.Handle(_validCommand);
        Assert.IsTrue(_handler.Valid);
        Assert.IsTrue(result.Success);
    }

    TodoHandler GetHandler() => new TodoHandler(new FakeTodoRepository());

}