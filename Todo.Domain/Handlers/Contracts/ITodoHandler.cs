using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Handlers.Contract;

public interface ITodoHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}