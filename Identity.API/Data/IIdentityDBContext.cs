using Identity.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Data;

public interface IIdentityDBContext
{
    DbSet<User> Users { get; set; }
}