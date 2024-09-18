using System.ComponentModel.DataAnnotations;

namespace TodoListBackend.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }

    public string RefreshToken { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Todo> TodoList { get; set; }
}