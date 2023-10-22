using Financial.API.DTOs;
using Financial.API.Services;
using Microsoft.AspNetCore.Mvc;
using Utilities.Enums;
using Utilities.Services;
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
    private readonly IErrorHandlerService _errorHandlerService;

    public BillController(
        ILogger<BillController> logger, 
        IBillService billService, 
        IErrorHandlerService errorHandlerService) 
        : base(logger)
    {
        _logger = logger;
        _billService = billService;
        _errorHandlerService = errorHandlerService;
    }
    
    # region Bill
    [HttpGet]
    public async Task<IActionResult> GetAllBillsForUser(Guid userId)
    {
        _logger.LogInformation("Getting all bills for user {UserId}", userId);
        
        if (userId == Guid.Empty)
            return BadRequest("UserId cannot be empty");

        try
        {
            var bills = await _billService.GetAllBillsForUser(userId);
            return Ok(bills);
        } catch (Exception e)
        {
            _logger.LogError(e, "Error getting bills for user {UserId}", userId);
            return StatusCode(500, "Error getting bills for user");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBills(List<BillDTO> bills)
    {
        _logger.LogInformation("Creating bills");
        
        try
        {
            var result = await _billService.CreateBills(bills);
        
            if (result.Result == ResultType.Success)
                return Created("/Financial/Bill/GetAllBillsForUser", null);   
            
            return BadRequest(result.Messages);
        } catch (Exception e)
        {
            _logger.LogError(e, "Error creating bills");
            return StatusCode(500, "Error creating bills");
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateBills(List<BillDTO> bills)
    {
        _logger.LogInformation("Updating bills");

        try
        {
            var result = await _billService.UpdateBills(bills);
        
            if (result.Result == ResultType.Success)
                return NoContent();
            
            return BadRequest(result.Messages);
        } catch (Exception e)
        {
            _logger.LogError(e, "Error updating bills");
            return StatusCode(500, "Error updating bills");
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteBill(Guid billId)
    {
        _logger.LogInformation("Deleting bill {BillId}", billId);

        if (billId == Guid.Empty)
        {
            var result = new StandardServiceResult(ResultType.Invalid, "BillId cannot be empty");
            return _errorHandlerService.GetErrorResponse(result);   
        }

        try
        {
            var result = await _billService.DeleteBill(billId);
            
            if (result.Result == ResultType.Success)
                return NoContent();   
            
            return BadRequest(result.Messages);
        } catch (Exception e)
        {
            _logger.LogError(e, "Error deleting bill {BillId}", billId);
            return StatusCode(500, "Error deleting bill");
        }
    }
    # endregion
}