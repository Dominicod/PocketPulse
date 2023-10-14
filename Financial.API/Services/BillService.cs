using Financial.API.DTOs;

namespace Financial.API.Services;

/// <summary>
/// Service for managing bills
/// </summary>
public class BillService : IBillService
{
    public async Task<string> GetAllBillsForUser(Guid userId)
    {
        return "got all bills for user";
    }
    
    public async Task<string> CreateBills(List<BillDTO> bills)
    {
        return "created bills";
    }
    
    public async Task<string> UpdateBills(List<BillDTO> bills)
    {
        return "updated bills";
    }
    
    public async Task<string> DeleteBills(List<Guid> billIds)
    {
        return "deleted bills";
    }
}