using System.ComponentModel.DataAnnotations;

namespace Utilities.Shared.DTOs;

public class BaseUserDTO : BaseDTO
{
    [Required]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
}