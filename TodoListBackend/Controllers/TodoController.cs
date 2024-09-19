using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListBackend.Data;
using TodoListBackend.Models;

namespace TodoListBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public TodoController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserTodos(int userId)
    {
        var todos = await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
        return Ok(todos);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTodo(Todo todo)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        todo.CreatedAt = DateTime.Now;
        todo.UpdatedAt = DateTime.Now;
        todo.UserId = userId;
        
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        
        return Ok(todo);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, Todo todo)
    {
        var existingTodo = await _context.Todos.FindAsync(id);
        
        if (existingTodo == null)
        {
            return NotFound();
        }
        
        existingTodo.Title = todo.Title;
        existingTodo.Description = todo.Description;
        existingTodo.IsCompleted = todo.IsCompleted;
        existingTodo.UpdatedAt = DateTime.Now;
        
        await _context.SaveChangesAsync();
        
        return Ok(existingTodo);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound();
        }
        
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}