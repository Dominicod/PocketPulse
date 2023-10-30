using Financial.API.Models;

namespace Financial.API.Repositories;

public interface IBillRepository
{
    Task<Bill?> GetBill(Guid billId);
    IQueryable<Bill> GetAllBills();
    Task<bool> CreateBill(Bill bill);
    Task<bool> CreateBills(List<Bill> bills);
    Task<bool> DeleteBill(Bill bill);
    Task<bool> DeleteBills(List<Bill> bills);
    Task<bool> UpdateBill(Bill bill);
    Task<bool> UpdateBills(List<Bill> bills);
}