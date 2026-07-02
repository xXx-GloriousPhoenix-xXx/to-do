using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TODO.Interfaces.Common;
using TODO.Interfaces.Common.CustomException;
using TODO.Interfaces.Todo;
using TODO.Interfaces.Todo.DTO;

namespace TODO.API.Controllers;

[ApiController]
[Route("todos")]
public class TodoController(ITodoService todoService) : ControllerBase
{
    private readonly ITodoService _todoService = todoService;

    [HttpGet]
    public async Task<ActionResult<PagedResponse<GetTodoDTO>>> GetAll(
        [FromQuery] TodoFilter? filter,
        [FromQuery] TodoSorter? sorter,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var userId = GetUserId();
        var result = await _todoService.GetAllAsync(userId, filter, sorter, pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetTodoDTO>> GetById(Guid id)
    {
        var userId = GetUserId();
        var result = await _todoService.GetByIdAsync(userId, id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GetTodoDTO>> Create(CreateTodoDTO dto)
    {
        var userId = GetUserId();
        var result = await _todoService.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTodoDTO dto)
    {
        var userId = GetUserId();
        await _todoService.UpdateAsync(userId, id, dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        await _todoService.DeleteAsync(userId, id);
        return NoContent();
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<string>>> GetCategories()
    {
        var userId = GetUserId();
        var result = await _todoService.GetCategoriesAsync(userId);
        return Ok(result);
    }

    private Guid GetUserId()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedException("Unauthorized");

        return Guid.Parse(idClaim.Value);
    }
}