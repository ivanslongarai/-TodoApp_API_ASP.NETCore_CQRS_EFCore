using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;

namespace Todo.Domain.Tests.CommandsTests;

[TestClass]
public class CreateTodoCommandsTests
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

    public CreateTodoCommandsTests()
    {
        _invalidCommand.Validate();
        _validCommand.Validate();
    }

    [TestMethod]
    public void Given_an_invalid_command_should_return_invalid()
    {
        Assert.IsFalse(_invalidCommand.Valid);
    }

    [TestMethod]
    public void Given_a_valid_command_should_return_valid()
    {
        Assert.IsTrue(_validCommand.Valid);
    }

}