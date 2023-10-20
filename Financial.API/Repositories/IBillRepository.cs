using Financial.API.Models;

namespace Financial.API.Repositories;

public interface IBillRepository
{
    Task<Bill?> GetBill(Guid billId);
    Task<List<Bill>> GetAllBills();
    Task<Bill> CreateBill(Bill bill);
    Task<List<Bill>> CreateBills(List<Bill> bills);
    Task<bool> DeleteBill(Bill bill);
    Task<bool> DeleteBills(List<Bill> bills);
    Task<Bill> UpdateBill(Bill bill);
    Task<List<Bill>> UpdateBills(List<Bill> bills);
}