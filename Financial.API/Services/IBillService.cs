using Financial.API.DTOs;
using Financial.API.Models;
using Utilities.Shared;

namespace Financial.API.Services;

public interface IBillService
{
    Task<IEnumerable<BillDTO>> GetAllBillsForUser(Guid userId);
    Task<StandardServiceResult> CreateBills(IEnumerable<BillDTO> bills);
    Task<StandardServiceResult> UpdateBills(IEnumerable<BillDTO> bills);
    Task<StandardServiceResult> DeleteBill(Guid billId);
}