using Utilities.Shared.DTOs;
using Utilities.Shared.Models;

namespace Identity.API.Models;

public class User : BaseUser
{
    public User()
    {
    }

    public User(BaseUserDTO userDTO)
    {
        Email = userDTO.Email;
        UserName = userDTO.UserName;
    }
}