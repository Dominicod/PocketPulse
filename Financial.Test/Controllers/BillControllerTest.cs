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
    private readonly Mock<IResponseHandlerService> _responseHandlerServiceMock = new();
    private readonly Faker _faker = new();
    private readonly BillController _controller;

    public BillControllerTest() => _controller = new BillController(_loggerMock.Object, _billServiceMock.Object, _responseHandlerServiceMock.Object);
    
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
        
        _responseHandlerServiceMock.Setup(r => r.GetOkResponse(bills))
            .Returns(new JsonResult(bills) { StatusCode = 200 });
        
        _billServiceMock.Setup(r => r.GetAllBillsForUser(userId))
            .ReturnsAsync(bills);
        
        // Act
        var result = await _controller.GetAllBillsForUser(userId);
        
        // Assert
        _billServiceMock.Verify(r => r.GetAllBillsForUser(userId), Times.Once);
        _responseHandlerServiceMock.Verify(r => r.GetOkResponse(bills), Times.Once);
        var dataResult = Assert.IsType<JsonResult>(result);
        Assert.Equal(200, dataResult.StatusCode);
        Assert.Equal(bills, dataResult.Value);
    }
    
    //? Sad Path
    //?
    [Fact]
    public async Task GetAllBillsForUser_ShouldReturnBadRequest_UserIdEmpty()
    {
        // Arrange
        var userId = Guid.Empty;
        
        _responseHandlerServiceMock.Setup(r => r.GetErrorResponse(It.IsAny<StandardServiceResult>()))
            .Returns(new JsonResult("UserId cannot be empty") { StatusCode = 400 });
        
        // Act
        var result = await _controller.GetAllBillsForUser(userId);
        
        // Assert
        _billServiceMock.Verify(r => r.GetAllBillsForUser(userId), Times.Never);
        _responseHandlerServiceMock.Verify(r => r.GetErrorResponse(It.Is<StandardServiceResult>(
            ssr => ssr.Result == ResultType.BadRequest && ssr.Messages.First() == "UserId cannot be empty")), Times.Once);
        var errorResult = Assert.IsType<JsonResult>(result);
        Assert.Equal("UserId cannot be empty", errorResult.Value);
        Assert.Equal(400, errorResult.StatusCode);
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
        
        _responseHandlerServiceMock.Setup(r => r.GetCreatedResponse())
            .Returns(new CreatedResult(string.Empty, null) { StatusCode = 201 });
        
        _billServiceMock.Setup(r => r.CreateBills(bills))
            .ReturnsAsync(standardServiceResult);
        
        // Act
        var result = await _controller.CreateBills(bills);
        
        // Assert
        _billServiceMock.Verify(r => r.CreateBills(bills), Times.Once);
        _responseHandlerServiceMock.Verify(r => r.GetCreatedResponse(), Times.Once);
        var dataResponse = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, dataResponse.StatusCode);
    }
    
    //? Sad Path
    //?
    [Fact]
    public async Task CreateBills_ShouldReturnFailure()
    {
        // Arrange
        var bills = BillFactory.BuildBillDTO()
            .Generate(5);
        var standardServiceResult = new StandardServiceResult(ResultType.Error, "Error has occured");
        
        _responseHandlerServiceMock.Setup(r => r.GetErrorResponse(It.IsAny<StandardServiceResult>()))
            .Returns(new JsonResult("Error has occured") { StatusCode = standardServiceResult.ParseResultToStatusCode() });
        
        _billServiceMock.Setup(r => r.CreateBills(bills))
            .ReturnsAsync(standardServiceResult);
        
        // Act
        var result = await _controller.CreateBills(bills);
        
        // Assert
        _billServiceMock.Verify(r => r.CreateBills(bills), Times.Once);
        _responseHandlerServiceMock.Verify(r => r.GetErrorResponse(It.Is<StandardServiceResult>(
            ssr => ssr.Result == ResultType.Error && ssr.Messages.First() == "Error has occured")), Times.Once);
        var errorResult = Assert.IsType<JsonResult>(result);
        Assert.Equal("Error has occured", errorResult.Value);
        Assert.Equal(standardServiceResult.ParseResultToStatusCode(), errorResult.StatusCode);
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
    
    //? Sad Path
    //?
    [Fact]
    public async Task UpdateBills_ShouldReturnFailure()
    {
        // Arrange
        var bills = BillFactory.BuildBillDTO()
            .Generate(5);
        var standardServiceResult = new StandardServiceResult(ResultType.Error, "Error has occured");
        
        _responseHandlerServiceMock.Setup(r => r.GetErrorResponse(It.IsAny<StandardServiceResult>()))
            .Returns(new JsonResult("Error has occured") { StatusCode = standardServiceResult.ParseResultToStatusCode() });
        
        _billServiceMock.Setup(r => r.UpdateBills(bills))
            .ReturnsAsync(standardServiceResult);
        
        // Act
        var result = await _controller.UpdateBills(bills);
        
        // Assert
        _billServiceMock.Verify(r => r.UpdateBills(bills), Times.Once);
        _responseHandlerServiceMock.Verify(r => r.GetErrorResponse(It.Is<StandardServiceResult>(
            ssr => ssr.Result == ResultType.Error && ssr.Messages.First() == "Error has occured")), Times.Once);
        var errorResult = Assert.IsType<JsonResult>(result);
        Assert.Equal("Error has occured", errorResult.Value);
        Assert.Equal(standardServiceResult.ParseResultToStatusCode(), errorResult.StatusCode);
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
    public async Task DeleteBill_ShouldReturnBadRequest_BillIdEmpty()
    {
        // Arrange
        var billId = Guid.Empty;
        
        _responseHandlerServiceMock.Setup(r => r.GetErrorResponse(It.IsAny<StandardServiceResult>()))
            .Returns(new JsonResult("BillId cannot be empty") { StatusCode = 400 });
        
        // Act
        var result = await _controller.DeleteBill(billId);
        
        // Assert
        _billServiceMock.Verify(r => r.DeleteBill(billId), Times.Never);
        _responseHandlerServiceMock.Verify(r => r.GetErrorResponse(It.Is<StandardServiceResult>(
            ssr => ssr.Result == ResultType.BadRequest && ssr.Messages.First() == "BillId cannot be empty")), Times.Once);
        var errorResult = Assert.IsType<JsonResult>(result);
        Assert.Equal("BillId cannot be empty", errorResult.Value);
        Assert.Equal(400, errorResult.StatusCode);
    }
    
    //? Sad Path
    //?
    [Fact]
    public async Task DeleteBills_ShouldReturnFailure()
    {
        // Arrange
        var billId = _faker.Random.Guid();
        var standardServiceResult = new StandardServiceResult(ResultType.Error, "Error has occured");
        
        _responseHandlerServiceMock.Setup(r => r.GetErrorResponse(It.IsAny<StandardServiceResult>()))
            .Returns(new JsonResult("Error has occured") { StatusCode = standardServiceResult.ParseResultToStatusCode() });
        
        _billServiceMock.Setup(r => r.DeleteBill(billId))
            .ReturnsAsync(standardServiceResult);
        
        // Act
        var result = await _controller.DeleteBill(billId);
        
        // Assert
        _billServiceMock.Verify(r => r.DeleteBill(billId), Times.Once);
        _responseHandlerServiceMock.Verify(r => r.GetErrorResponse(It.Is<StandardServiceResult>(
            ssr => ssr.Result == ResultType.Error && ssr.Messages.First() == "Error has occured")), Times.Once);
        var errorResult = Assert.IsType<JsonResult>(result);
        Assert.Equal("Error has occured", errorResult.Value);
        Assert.Equal(standardServiceResult.ParseResultToStatusCode(), errorResult.StatusCode);
    }
    # endregion
}