using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers;

[ApiController]
[Route("v1/todos")]
[Authorize]
public class TodoController : ControllerBase
{

    // CQRS
    // For writing, it uses "Commands and Handlers"
    // For reading, it uses "Repository Queries"

    //GetAll
    [Route("")]
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetAll(
        [FromServices] ITodoRepository repository
    )
    {
        var user = repository.GetAll(GetUser());
        return Ok(user);
    }

    //GetAllDone
    [Route("done")]
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetAllDone(
        [FromServices] ITodoRepository repository
    )
    {
        var result = repository.GetAllDone(GetUser());
        return Ok(result);
    }

    //GetAllUndone
    [Route("undone")]
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetAllUndone(
            [FromServices] ITodoRepository repository
        )
    {
        var result = repository.GetAllUndone(GetUser());
        return Ok(result);
    }

    //GetDoneToday
    [Route("done/today")]
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetDoneToday(
    [FromServices] ITodoRepository repository
)
    {
        var result = repository.GetByDate(GetUser(), DateTime.UtcNow.Date, true);
        return Ok(result);
    }

    //GetUndoneToday
    [Route("undone/today")]
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetUndoneToday(
        [FromServices] ITodoRepository repository
    )
    {
        var result = repository.GetByDate(GetUser(), DateTime.UtcNow.Date, false);
        return Ok(result);
    }

    //GetDoneTomorrow
    [Route("done/tomorrow")]
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetDoneTomorrow(
        [FromServices] ITodoRepository repository
    )
    {
        var result = repository.GetByDate(GetUser(), DateTime.UtcNow.Date.AddDays(1), true);
        return Ok(result);
    }

    //GetUndoneTomorrow
    [Route("undone/tomorrow")]
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetUndoneTomorrow(
        [FromServices] ITodoRepository repository
    )
    {
        var result = repository.GetByDate(GetUser(), DateTime.UtcNow.Date.AddDays(1), false);
        return Ok(result);
    }

    //Create
    [Route("")]
    [HttpPost]
    public ActionResult<GenericCommandResult> Create(
        [FromBody] CreateTodoCommand command,
        [FromServices] TodoHandler handler
    )
    {
        command.User = GetUser();
        var result = (GenericCommandResult)handler.Handle(command);
        return Ok(result);
    }

    //Update
    [Route("")]
    [HttpPut]
    public ActionResult<GenericCommandResult> Update(
        [FromBody] UpdateTodoCommand command,
        [FromServices] TodoHandler handler
    )
    {
        command.User = GetUser();
        var result = (GenericCommandResult)handler.Handle(command);
        return Ok(result);
    }

    //SetAsDone
    [Route("set-as-done")]
    [HttpPut]
    public ActionResult<GenericCommandResult> SetAsDone(
        [FromBody] SetTodoAsDoneCommand command,
        [FromServices] TodoHandler handler
    )
    {
        command.User = GetUser();
        var result = (GenericCommandResult)handler.Handle(command);
        return Ok(result);
    }

    //SetAsUndone
    [Route("set-as-undone")]
    [HttpPut]
    public ActionResult<GenericCommandResult> SetAsUndone(
        [FromBody] SetTodoAsUndoneCommand command,
        [FromServices] TodoHandler handler
    )
    {
        command.User = GetUser();
        var result = (GenericCommandResult)handler.Handle(command);
        return Ok(result);
    }

    //Delete
    [Route("")]
    [HttpDelete]
    public ActionResult<GenericCommandResult> Delete(
        [FromBody] DeleteTodoCommand command,
        [FromServices] TodoHandler handler
    )
    {
        command.User = GetUser();
        var result = (GenericCommandResult)handler.Handle(command);
        return Ok(result);
    }

    private string GetUser()
    {
        return User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
    }
}
