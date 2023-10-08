using Financial.API.DTOs;
using Microsoft.AspNetCore.Mvc;
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

    public BillController(ILogger<BillController> logger) : base(logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBillsForUser(Guid userId)
    {
        _logger.LogInformation("Getting all bills for user {UserId}", userId);
        return Ok("Hello World");
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBills(List<BillDTO> bills)
    {
        _logger.LogInformation("Creating bills");
        return Created("Hello World", Guid.NewGuid());
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateBills(List<BillDTO> bills)
    {
        _logger.LogInformation("Updating bills");
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteBills(List<Guid> billIds)
    {
        _logger.LogInformation("Deleting bills");
        return NoContent();
    }
}