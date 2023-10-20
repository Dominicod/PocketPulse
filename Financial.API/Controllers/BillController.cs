using Financial.API.DTOs;
using Financial.API.Services;
using Microsoft.AspNetCore.Mvc;
using Utilities.Enums;
using Utilities.Shared;

namespace Financial.API.Controllers;

/// <summary>
/// Controller for managing bills
/// API Documentation: https://www.postman.com/dodonnell99/workspace/pocketpulseapi/folder/23643166-c195520b-6e40-43e0-bb14-328842fa53ff
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class BillController : BaseController<BillController>
{
    private readonly ILogger<BillController> _logger;
    private readonly IBillService _billService;

    public BillController(
        ILogger<BillController> logger, 
        IBillService billService) : base(logger)
    {
        _logger = logger;
        _billService = billService;
    }
    
    # region Bill
    [HttpGet]
    public async Task<IActionResult> GetAllBillsForUser(Guid userId)
    {
        _logger.LogInformation("Getting all bills for user {UserId}", userId);
        
        if (userId == Guid.Empty)
            return BadRequest("UserId cannot be empty");
        
        var bills = await _billService.GetAllBillsForUser(userId);
        
        return Ok(bills);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBills(List<BillDTO> bills)
    {
        _logger.LogInformation("Creating bills");
        
        await _billService.CreateBills(bills);
        
        return Created("/Financial/Bill/GetAllBillsForUser", null);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateBills(List<BillDTO> bills)
    {
        _logger.LogInformation("Updating bills");
        
        await _billService.UpdateBills(bills);
        
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteBill(Guid billId)
    {
        _logger.LogInformation("Deleting bill {BillId}", billId);
        
        if (billId == Guid.Empty)
            return BadRequest("BillId cannot be empty");
        
        await _billService.DeleteBill(billId);
        
        return NoContent();
    }
    # endregion
}