using System.Data;
using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contract;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers;


public class TodoHandler :
    Notifiable,
    ITodoHandler<CreateTodoCommand>,
    ITodoHandler<UpdateTodoCommand>,
    ITodoHandler<SetTodoAsDoneCommand>,
    ITodoHandler<SetTodoAsUndoneCommand>,
    ITodoHandler<DeleteTodoCommand>
{
    private readonly ITodoRepository _repository;

    public TodoHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    //CreateTodoCommand
    public ICommandResult Handle(CreateTodoCommand command)
    {
        command.Validate();
        if (command.Invalid)
            return GetInvalidCommandResult(command.Notifications);

        var todo = new TodoItem(
            command.Title,
            command.User!,
            command.ExecutionDate
        );

        if (_repository.Create(todo))
            return new GenericCommandResult(
                true,
                "Todo was created successfully",
                todo,
                null);

        return GetDBFailedResult();
    }

    //UpdateTodoCommand
    public ICommandResult Handle(UpdateTodoCommand command)
    {
        command.Validate();
        if (command.Invalid)
            return GetInvalidCommandResult(command.Notifications);

        var todo = _repository.GetById(command.Id, command.User!);
        if (todo == null)
            return GetTodoNotFoundResult(command.Id, command.User!);

        todo.UpdateTile(command.Title);

        if (command.ExecutionDate.Date >= todo.CreatedAt.Date)
            todo.UpdateExecutionDate(command.ExecutionDate);
        else
        {
            AddNotification("Todo", "Invalid ExecutionDate");
            return new GenericCommandResult(
                false,
                "oops, something went wrong",
                null,
                "the execution date has to be greater than todo created date");
        }

        if (_repository.Update(todo))
            return new GenericCommandResult(
                true,
                "Todo was updated successfully",
                new
                {
                    Id = command.Id,
                    Title = command.Title,
                    User = command.User,
                    ExecutionDate = command.ExecutionDate
                },
                null);

        return GetDBFailedResult();
    }

    //SetTodoAsDoneCommand
    public ICommandResult Handle(SetTodoAsDoneCommand command)
    {
        command.Validate();
        if (command.Invalid)
            return GetInvalidCommandResult(command.Notifications);

        var todo = _repository.GetById(command.Id, command.User!);
        if (todo == null)
            return GetTodoNotFoundResult(command.Id, command.User!);

        todo.SetAsDone();

        if (_repository.Update(todo))
            return new GenericCommandResult(
                true,
                "Todo was set as done successfully",
                todo,
                null);

        return GetDBFailedResult();
    }

    //SetTodoAsUndoneCommand
    public ICommandResult Handle(SetTodoAsUndoneCommand command)
    {
        command.Validate();
        if (command.Invalid)
            return GetInvalidCommandResult(command.Notifications);

        var todo = _repository.GetById(command.Id, command.User!);
        if (todo == null)
            return GetTodoNotFoundResult(command.Id, command.User!);

        todo.SetAsUndone();

        if (_repository.Update(todo))
            return new GenericCommandResult(
                true,
                "Todo was set as undone successfully",
                todo,
                null);

        return GetDBFailedResult();
    }

    //DeleteTodoCommand
    public ICommandResult Handle(DeleteTodoCommand command)
    {
        command.Validate();
        if (command.Invalid)
            return GetInvalidCommandResult(command.Notifications);


        var todo = _repository.GetById(command.Id, command.User!);
        if (todo == null)
            return GetTodoNotFoundResult(command.Id, command.User!);

        if (_repository.Delete(todo))
            return new GenericCommandResult(
                true,
                "Todo was deleted successfully",
                new
                {
                    Id = command.Id,
                    User = command.User
                },
                null);

        return GetDBFailedResult();
    }

    public GenericCommandResult GetInvalidCommandResult(IReadOnlyCollection<Notification> notifications)
    {
        AddNotification("Command", "Invalid Command");
        return new GenericCommandResult(
            false,
            "oops, something went wrong",
            null,
            notifications);
    }

    public GenericCommandResult GetDBFailedResult()
    {
        AddNotification("DB", "DB has failed");
        return new GenericCommandResult(
            false,
            "oops, DB has failed",
            null,
            null);
    }

    public GenericCommandResult GetTodoNotFoundResult(Guid id, string user)
    {
        AddNotification("Todo", $"todo {id} {user} was not found");
        return new GenericCommandResult(
            false,
            "oops, something went wrong",
            null,
            $"todo {id} {user} was not found");
    }

}