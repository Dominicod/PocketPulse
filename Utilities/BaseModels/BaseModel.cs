using System.ComponentModel.DataAnnotations;

namespace Utilities.BaseModels;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}