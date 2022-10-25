using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoer.Models;
using ToDoer.Services;

namespace ToDoer.Controllers;

[ApiController]
[Route("todo/")]
public class TodoController : Controller
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IEnumerable<ToDoList>> Get()
        => await _todoService.GetAsync();

    [HttpGet("{owner:length(64)}")]
    public async Task<List<ToDoList>> GetByOwner(string owner)
        => await _todoService.GetByOwnerAsync(owner);

    [HttpPost]
    public async Task<IActionResult> Post(ToDoList toDoList)
    {
        await _todoService.CreateAsync(toDoList);
        return CreatedAtAction(nameof(Get), new { id = toDoList.Id, toDoList });
    }

    [HttpPut("upd/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, ToDoList toDoList)
    {
        var todo = await _todoService.GetAsync(id);
        if (todo is null) return NotFound();
        toDoList.Id = todo.Id;
        await _todoService.UpdateAsync(id, toDoList);
        return NoContent();
    }

    [HttpDelete("del/{id:length(24)}")]
    public async Task<IActionResult> Remove(string id)
    {
        var todo = await _todoService.GetAsync(id);
        if (todo is null) return NotFound();
        await _todoService.RemoveAsync(id);
        return NoContent();
    }
}