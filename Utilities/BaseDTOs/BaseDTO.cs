using System.ComponentModel.DataAnnotations;

namespace Utilities.BaseDTOs;

public class BaseDTO
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}