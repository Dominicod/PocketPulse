using Financial.API.Data;
using Financial.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Financial.API.Repositories;

public class BillRepository : IBillRepository
{
    private readonly FinancialDBContext _context;

    public BillRepository(FinancialDBContext context) => _context = context;

    # region Bill
    public async Task<Bill?> GetBill(Guid billId) => await _context.Bills.FindAsync(billId);

    public IQueryable<Bill> GetAllBills() => _context.Bills.OrderBy(i => i.Id);

    public async Task<bool> CreateBill(Bill bill)
    {
        await _context.Bills.AddAsync(bill);
        var savedChangeCount = await _context.SaveChangesAsync();

        return savedChangeCount >= 1;
    }
    
    public async Task<bool> CreateBills(List<Bill> bills)
    {
        await _context.Bills.AddRangeAsync(bills);
        var savedChangeCount = await _context.SaveChangesAsync();

        return savedChangeCount >= bills.Count;
    }

    public async Task<bool> DeleteBill(Bill bill)
    {
        _context.Remove(bill);
        var savedChangeCount = await _context.SaveChangesAsync();

        return savedChangeCount >= 1;
    }
    
    public async Task<bool> DeleteBills(List<Bill> bills)
    {
        _context.RemoveRange(bills);
        var savedChangeCount = await _context.SaveChangesAsync();

        return savedChangeCount >= bills.Count;
    }

    public async Task<bool> UpdateBill(Bill bill)
    {
        _context.Bills.Update(bill);
        var savedChangeCount = await _context.SaveChangesAsync();
            
        return savedChangeCount >= 1;
    }

    public async Task<bool> UpdateBills(List<Bill> bills)
    {
        _context.Bills.UpdateRange(bills);
        var savedChangeCount = await _context.SaveChangesAsync();
            
        return savedChangeCount >= bills.Count;
    }
    # endregion
}