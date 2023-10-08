using Financial.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Financial.API.Data;

public interface IFinancialDBContext
{
    DbSet<Bill> Bills { get; set; }
}