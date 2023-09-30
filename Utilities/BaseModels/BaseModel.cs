using System.ComponentModel.DataAnnotations;

namespace Utilities.Shared.Models;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}