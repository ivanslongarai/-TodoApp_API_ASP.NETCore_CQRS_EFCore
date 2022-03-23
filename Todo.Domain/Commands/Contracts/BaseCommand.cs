using Flunt.Notifications;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands;

public abstract class BaseCommand : Notifiable, ICommand
{
    public abstract bool Validate();
}