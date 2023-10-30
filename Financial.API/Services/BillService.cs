using Financial.API.DTOs;
using Financial.API.Models;
using Financial.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Utilities.Enums;
using Utilities.Shared;

namespace Financial.API.Services;

/// <summary>
/// Service for managing bills
/// </summary>
public class BillService : IBillService
{
    private readonly IBillRepository _billRepository;
    
    public BillService(IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }
    
    # region Bills
    public async Task<IEnumerable<BillDTO>> GetAllBillsForUser(Guid userId)
    {
        var bills = _billRepository.GetAllBills()
            .Where(b => b.UserId == userId);
        var billsDTO = await bills.Select(b => new BillDTO(b)).ToListAsync();

        return billsDTO;
    }
    
    public async Task<StandardServiceResult> CreateBills(IEnumerable<BillDTO> bills)
    {
        var billModels = bills.Select(b => new Bill(b)).ToList();
        var result = await _billRepository.CreateBills(billModels);
        
        return result ? new StandardServiceResult(ResultType.Success) : new StandardServiceResult(ResultType.Error, "Failed to create bills");
    }

    public async Task<StandardServiceResult> DeleteBill(Guid billId)
    {
        var billModel = await _billRepository.GetBill(billId);

        if (billModel == null)
            return new StandardServiceResult(ResultType.NotFound, "Bill not found");

        var result = await _billRepository.DeleteBill(billModel);
        
        return result ? new StandardServiceResult(ResultType.Success) : new StandardServiceResult(ResultType.Error, "Failed to delete bill");
    }
    
    public async Task<StandardServiceResult> UpdateBills(IEnumerable<BillDTO> bills)
    {
        var billModels = bills.Select(b => new Bill(b)).ToList();
        var result = await _billRepository.UpdateBills(billModels);
        
        return result ? new StandardServiceResult(ResultType.Success) : new StandardServiceResult(ResultType.Error, "Failed to update bills");
    }
    # endregion
}