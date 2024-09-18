using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListBackend.Models;

public class Todo
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    
    [StringLength(400)]
    public string Description { get; set; }
    
    [Required]
    public bool IsCompleted { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    public User User { get; set; }
}