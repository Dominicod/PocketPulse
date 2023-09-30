using System.ComponentModel.DataAnnotations;

namespace Utilities.Shared.Models;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set; }
}