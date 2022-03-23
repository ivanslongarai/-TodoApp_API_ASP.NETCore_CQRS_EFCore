using Flunt.Validations;

namespace Todo.Domain.Commands;

public class SetTodoAsDoneCommand : BaseCommand
{
    public SetTodoAsDoneCommand(Guid id, string? user)
    {
        Id = id;
        User = user;
    }

    public Guid Id { get; set; }
    public string? User { get; set; }

    public override bool Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .IsTrue(!string.IsNullOrEmpty(User), "Todo.User", "Invalid - Null or Empty")
        );
        return Valid;
    }
}