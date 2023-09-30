using System.ComponentModel.DataAnnotations;
using Identity.API.Models;
using Utilities.Shared.DTOs;

namespace Identity.API.DTOs;

public class UserDTO : BaseDTO
{
    [Required]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
    
    public UserDTO()
    {
    }

    public UserDTO(User user)
    {
        Id = user.Id;
        Email = user.Email;
        UserName = user.UserName;
        
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
    }
}