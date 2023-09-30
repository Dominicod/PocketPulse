using System.ComponentModel.DataAnnotations;
using Identity.API.DTOs;
using Utilities.Shared.Models;

namespace Identity.API.Models;

public class User : BaseModel
{
    [Required]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
    
    public User()
    {
    }

    public User(UserDTO userDTO)
    {
        Id = userDTO.Id;
        Email = userDTO.Email;
        UserName = userDTO.UserName;
    }
}