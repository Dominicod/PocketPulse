using Financial.API.Models;
using Microsoft.EntityFrameworkCore;
using Utilities.Shared;

namespace Financial.API.Data;

public class FinancialDBContext : BaseDataContext, IFinancialDBContext
{
    public FinancialDBContext()
    {
    }
    
    public FinancialDBContext(DbContextOptions<FinancialDBContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Financial");
    }
    
    public DbSet<Bill> Bills { get; set; } = null!;
}