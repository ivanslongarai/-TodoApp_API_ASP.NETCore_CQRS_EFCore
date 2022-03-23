using System;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands;

public class CreateTodoCommand : BaseCommand
{
    public CreateTodoCommand(string title, DateTime executionDate, string? user)
    {
        Title = title;
        ExecutionDate = executionDate;
        User = user;
    }

    public string Title { get; set; }
    public DateTime ExecutionDate { get; set; }
    public string? User { get; set; }

    public override bool Validate()
    {
        var titleMinLen = 5;
        var titleMaxLen = 50;

        AddNotifications(new Contract()
            .Requires()
            .IsTrue(!string.IsNullOrEmpty(Title), "Todo.Title", "Invalid - Null or Empty")
            .HasMinLen(Title, titleMinLen, "Todo.Title", $"Invalid - MinLen ({titleMinLen})")
            .HasMaxLen(Title, titleMaxLen, "Todo.Title", $"Invalid - MaxLen ({titleMaxLen})")
            .IsTrue(ExecutionDate.Date >= DateTime.UtcNow.Date, "Todo.ExecutionDate", "Invalid - It has to be greater than now")
            .IsTrue(!string.IsNullOrEmpty(User), "Todo.User", "Invalid - Null or Empty")
        );

        return Valid;
    }
}