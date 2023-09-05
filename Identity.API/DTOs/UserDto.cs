using Utilities.Shared.DTOs;
using Utilities.Shared.Models;

namespace Identity.API.DTOs;

public class UserDTO : BaseUserDTO
{
    public UserDTO()
    {
    }

    public UserDTO(BaseUser user)
    {
        Email = user.Email;
        UserName = user.UserName;
        
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
    }
}