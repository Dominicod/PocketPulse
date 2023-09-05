using System.ComponentModel.DataAnnotations;

namespace Utilities.Shared.Models;

public class BaseUser : BaseModel
{
    [Required]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
}