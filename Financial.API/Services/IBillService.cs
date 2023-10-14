using Financial.API.DTOs;

namespace Financial.API.Services;

public interface IBillService
{
    Task<string> GetAllBillsForUser(Guid userId);
    Task<string> CreateBills(List<BillDTO> bills);
    Task<string> UpdateBills(List<BillDTO> bills);
    Task<string> DeleteBills(List<Guid> billIds);
}