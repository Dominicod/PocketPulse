using System.ComponentModel.DataAnnotations;

namespace Utilities.Shared.DTOs;

public class BaseDTO
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set; }
}