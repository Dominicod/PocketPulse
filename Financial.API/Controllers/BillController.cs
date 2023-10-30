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
    private readonly IResponseHandlerService _responseHandlerService;

    public BillController(
        ILogger<BillController> logger, 
        IBillService billService, 
        IResponseHandlerService responseHandlerService) 
        : base(logger)
    {
        _logger = logger;
        _billService = billService;
        _responseHandlerService = responseHandlerService;
    }
    
    # region Bill
    [HttpGet]
    public async Task<IActionResult> GetAllBillsForUser(Guid userId)
    {
        _logger.LogInformation("Getting all bills for user {UserId}", userId);
        
        if (userId == Guid.Empty)
            return _responseHandlerService.GetErrorResponse(new StandardServiceResult(ResultType.BadRequest, "UserId cannot be empty"));

        try
        {
            var bills = await _billService.GetAllBillsForUser(userId);
            return _responseHandlerService.GetOkResponse(bills);
        } catch (Exception e)
        {
            return _responseHandlerService.GetErrorResponse(e);
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
                return _responseHandlerService.GetCreatedResponse("/Bill/GetAllBillsForUser?userId=" + bills[0].UserId);  
            
            return _responseHandlerService.GetErrorResponse(result);
        } catch (Exception e)
        {
            return _responseHandlerService.GetErrorResponse(e);
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
            
            return _responseHandlerService.GetErrorResponse(result);
        } catch (Exception e)
        {
            return _responseHandlerService.GetErrorResponse(e);
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteBill(Guid billId)
    {
        _logger.LogInformation("Deleting bill {BillId}", billId);

        if (billId == Guid.Empty)
            return _responseHandlerService.GetErrorResponse(new StandardServiceResult(ResultType.BadRequest, "BillId cannot be empty"));

        try
        {
            var result = await _billService.DeleteBill(billId);
            
            if (result.Result == ResultType.Success)
                return NoContent();   
            
            return _responseHandlerService.GetErrorResponse(result);
        } catch (Exception e)
        {
           return _responseHandlerService.GetErrorResponse(e);
        }
    }
    # endregion
}