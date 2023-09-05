using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Utilities.Shared;

namespace Identity.API.Data;

public class IdentityDBContext : BaseDataContext, IIdentityDBContext
{
    public IdentityDBContext()
    {
    }
    
    public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}