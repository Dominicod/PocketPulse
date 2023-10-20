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
        await _billRepository.CreateBills(billModels);
        
        return new StandardServiceResult(ResultType.Success);
    }

    public async Task<StandardServiceResult> DeleteBill(Guid billId)
    {
        var billModel = await _billRepository.GetBill(billId);

        if (billModel == null)
            return new StandardServiceResult(ResultType.Failure, "Bill not found");

        await _billRepository.DeleteBill(billModel);
        
        return new StandardServiceResult(ResultType.Success);
    }
    
    public async Task<StandardServiceResult> UpdateBills(IEnumerable<BillDTO> bills)
    {
        var billModels = bills.Select(b => new Bill(b)).ToList();
        await _billRepository.UpdateBills(billModels);
        
        return new StandardServiceResult(ResultType.Success);
    }
    # endregion
}