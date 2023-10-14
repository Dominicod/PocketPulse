using Financial.API.Controllers;
using Financial.API.Services;
using Financial.Test.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Financial.Test.Controllers;

public class BillControllerTest
{
    private readonly Mock<ILogger<BillController>> _loggerMock = new();
    private readonly Mock<IBillService> _billServiceMock = new();
    private readonly Faker _faker = new();
    private readonly BillController _controller;

    public BillControllerTest() => _controller = new BillController(_loggerMock.Object, _billServiceMock.Object);
    
    //? Happy Path
    //?
    [Fact]
    public async Task GetAllBillsForUser_ShouldReturnOk()
    {
        // Arrange
        var userId = _faker.Random.Guid();
        
        // Act
        var result = await _controller.GetAllBillsForUser(userId);
        
        // Assert
        _billServiceMock.Verify(r => r.GetAllBillsForUser(userId), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task CreateBills_ShouldReturnCreated()
    {
        // Arrange
        var bills = BillFactory.BuildBillDTO()
            .Generate(5);
        
        // Act
        var result = await _controller.CreateBills(bills);
        
        // Assert
        _billServiceMock.Verify(r => r.CreateBills(bills), Times.Once);
        Assert.IsType<CreatedResult>(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task UpdateBills_ShouldReturnNoContent()
    {
        // Arrange
        var bills = BillFactory.BuildBillDTO()
            .Generate(5);
        
        // Act
        var result = await _controller.UpdateBills(bills);
        
        // Assert
        _billServiceMock.Verify(r => r.UpdateBills(bills), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task DeleteBills_ShouldReturnNoContent()
    {
        // Arrange
        var billIds = _faker.Make(5, () => _faker.Random.Guid())
            .ToList();
        
        // Act
        var result = await _controller.DeleteBills(billIds);
        
        // Assert
        _billServiceMock.Verify(r => r.DeleteBills(billIds), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
}