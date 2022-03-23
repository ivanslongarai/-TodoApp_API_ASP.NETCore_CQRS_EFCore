using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands;

public class GenericCommandResult : ICommandResult
{
    public GenericCommandResult() { }

    public GenericCommandResult(bool success, string? message, object? data, object? notifications)
    {
        Success = success;
        Message = message;
        Data = data;
        Notifications = notifications;
    }

    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public object? Notifications { get; set; }
}