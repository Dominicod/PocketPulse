using Financial.API.Data;
using Financial.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Financial.API.Repositories;

public class BillRepository : IBillRepository
{
    private readonly FinancialDBContext _context;

    public BillRepository(FinancialDBContext context) => _context = context;

    public async Task<Bill?> GetBill(Guid billId) => await _context.Bills.FindAsync(billId);

    public IQueryable<Bill> GetAllBills() => _context.Bills.OrderBy(i => i.Id);

    public async Task<Bill> CreateBill(Bill bill)
    {
        await _context.Bills.AddAsync(bill);
        await _context.SaveChangesAsync();

        return bill;
    }
    
    public async Task<List<Bill>> CreateBills(List<Bill> bills)
    {
        await _context.Bills.AddRangeAsync(bills);
        await _context.SaveChangesAsync();

        return bills;
    }

    public async Task<bool> DeleteBill(Bill bill)
    {
        _context.Remove(bill);
        var result = await _context.SaveChangesAsync();

        return result >= 1;
    }
    
    public async Task<bool> DeleteBills(List<Bill> bills)
    {
        _context.RemoveRange(bills);
        var result = await _context.SaveChangesAsync();

        return result == bills.Count;
    }

    public async Task<Bill> UpdateBill(Bill bill)
    {
        _context.Bills.Update(bill);
        await _context.SaveChangesAsync();
            
        return bill;
    }

    public async Task<List<Bill>> UpdateBills(List<Bill> bills)
    {
        _context.Bills.UpdateRange(bills);
        await _context.SaveChangesAsync();
            
        return bills;
    }
}