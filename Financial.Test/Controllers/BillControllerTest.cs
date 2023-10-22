using Financial.API.Controllers;
using Financial.API.Services;
using Financial.Test.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Utilities.Enums;
using Utilities.Services;
using Utilities.Shared;

namespace Financial.Test.Controllers;

public class BillControllerTest
{
    private readonly Mock<ILogger<BillController>> _loggerMock = new();
    private readonly Mock<IBillService> _billServiceMock = new();
    private readonly Mock<IErrorHandlerService> _errorHandlerServiceMock = new();
    private readonly Faker _faker = new();
    private readonly BillController _controller;

    public BillControllerTest() => _controller = new BillController(_loggerMock.Object, _billServiceMock.Object, _errorHandlerServiceMock.Object);
    
    # region Bill
    //? Happy Path
    //?
    [Fact]
    public async Task GetAllBillsForUser_ShouldReturnOk()
    {
        // Arrange
        var userId = _faker.Random.Guid();
        var bills = BillFactory.BuildBillDTO()
            .Generate(5);
        
        _billServiceMock.Setup(r => r.GetAllBillsForUser(userId))
            .ReturnsAsync(bills);
        
        // Act
        var result = await _controller.GetAllBillsForUser(userId);
        
        // Assert
        _billServiceMock.Verify(r => r.GetAllBillsForUser(userId), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }
    
    //? Sad Path
    //?
    [Fact]
    public async Task GetAllBillsForUser_ShouldReturnBadRequest()
    {
        // Arrange
        var userId = Guid.Empty;
        
        // Act
        var result = await _controller.GetAllBillsForUser(userId);
        
        // Assert
        _billServiceMock.Verify(r => r.GetAllBillsForUser(userId), Times.Never);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task CreateBills_ShouldReturnCreated()
    {
        // Arrange
        var bills = BillFactory.BuildBillDTO()
            .Generate(5);
        var standardServiceResult = new StandardServiceResult(ResultType.Success);
        
        _billServiceMock.Setup(r => r.CreateBills(bills))
            .ReturnsAsync(standardServiceResult);
        
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
        var standardServiceResult = new StandardServiceResult(ResultType.Success);
        
        _billServiceMock.Setup(r => r.UpdateBills(bills))
            .ReturnsAsync(standardServiceResult);
        
        // Act
        var result = await _controller.UpdateBills(bills);
        
        // Assert
        _billServiceMock.Verify(r => r.UpdateBills(bills), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task DeleteBill_ShouldReturnNoContent()
    {
        // Arrange
        var billId = _faker.Random.Guid();
        var standardServiceResult = new StandardServiceResult(ResultType.Success);
        
        _billServiceMock.Setup(r => r.DeleteBill(billId))
            .ReturnsAsync(standardServiceResult);
        
        // Act
        var result = await _controller.DeleteBill(billId);
        
        // Assert
        _billServiceMock.Verify(r => r.DeleteBill(billId), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
    
    //? Sad Path
    //?
    [Fact]
    public async Task DeleteBill_ShouldReturnBadRequest()
    {
        // Arrange
        var billId = Guid.Empty;
        
        // Act
        var result = await _controller.DeleteBill(billId);
        
        // Assert
        _billServiceMock.Verify(r => r.DeleteBill(billId), Times.Never);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    # endregion
}