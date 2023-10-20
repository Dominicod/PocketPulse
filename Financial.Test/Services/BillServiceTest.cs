using Financial.API.Models;
using Financial.API.Repositories;
using Financial.API.Services;
using Financial.Test.Factories;
using MockQueryable.Moq;
using Moq;
using Utilities.Enums;
using Utilities.Shared;

namespace Financial.Test.Services;

public class BillServiceTest
{
    private readonly Faker _faker = new();
    private readonly Mock<IBillRepository> _billRepositoryMock = new();
    private readonly BillService _service;
    
    public BillServiceTest()
    {
        _service = new BillService(_billRepositoryMock.Object);
    }

    //? Happy Path
    //?
    [Fact]
    public async Task GetAllBillsForUser_ShouldReturnString()
    {
        // Arrange
        var userId = _faker.Random.Guid();
        var bills = BillFactory.BuildBill()
            .RuleFor(i => i.UserId, userId)
            .Generate(5)
            .BuildMock();
        
        _billRepositoryMock.Setup(x => x.GetAllBills())
            .Returns(bills);
        
        // Act
        var result = await _service.GetAllBillsForUser(userId);
        var billsList = bills.ToList();
        
        // Assert
        Assert.NotEmpty(billsList);
        Assert.Equal(bills.Count(), result.Count());
    }
    
    //? Sad Path
    //?
    [Fact]
    public async Task GetAllBillsForUser_ShouldReturnEmptyList()
    {
        // Arrange
        var userId = _faker.Random.Guid();
        var bills = BillFactory.BuildBill()
            .RuleFor(i => i.UserId, _faker.Random.Guid())
            .Generate(5)
            .BuildMock();
        
        _billRepositoryMock.Setup(x => x.GetAllBills())
            .Returns(bills);
        
        // Act
        var result = await _service.GetAllBillsForUser(userId);
        
        // Assert
        Assert.Empty(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task CreateBills_Should_StandardServiceResult_Success()
    {
        // Arrange
        var billDTOs = BillFactory.BuildBillDTO()
            .RuleFor(i => i.Id, Guid.Empty)
            .Generate(5);
        var billModels = billDTOs.Select(b => new Bill(b) { Id = Guid.NewGuid() }).ToList();
        
        _billRepositoryMock.Setup(x => x.CreateBills(It.IsAny<List<Bill>>()))
            .ReturnsAsync(billModels);
        
        // Act
        var result = await _service.CreateBills(billDTOs);
        
        // Assert
        _billRepositoryMock.Verify(x => x.CreateBills(It.IsAny<List<Bill>>()), Times.Once);
        Assert.NotNull(result);
        Assert.IsType<StandardServiceResult>(result);
        Assert.Equal(ResultType.Success, result.Result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task UpdateBills_ShouldReturn_StandardServiceResult_Success()
    {
        // Arrange
        var billDTOs = BillFactory.BuildBillDTO()
            .RuleFor(i => i.Id, Guid.Empty)
            .Generate(5);
        var billModels = billDTOs.Select(b => new Bill(b) { Id = Guid.NewGuid() }).ToList();
        
        _billRepositoryMock.Setup(x => x.UpdateBills(It.IsAny<List<Bill>>()))
            .ReturnsAsync(billModels);
        
        // Act
        var result = await _service.UpdateBills(billDTOs);
        
        // Assert
        _billRepositoryMock.Verify(x => x.UpdateBills(It.IsAny<List<Bill>>()), Times.Once);
        Assert.NotNull(result);
        Assert.IsType<StandardServiceResult>(result);
        Assert.Equal(ResultType.Success, result.Result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task DeleteBill_ShouldReturn_StandardServiceResult_Success()
    {
        // Arrange
        var bill = BillFactory.BuildBill()
            .Generate();
        
        _billRepositoryMock.Setup(x => x.GetBill(bill.Id))
            .ReturnsAsync(bill);
        
        // Act
        var result = await _service.DeleteBill(bill.Id);
        
        // Assert
        _billRepositoryMock.Verify(x => x.GetBill(bill.Id), Times.Once);
        _billRepositoryMock.Verify(x => x.DeleteBill(bill), Times.Once);
        Assert.NotNull(result);
        Assert.IsType<StandardServiceResult>(result);
        Assert.Equal(ResultType.Success, result.Result);
    }
    
    //? Sad Path
    //?
    [Fact]
    public async Task DeleteBill_ShouldReturn_StandardServiceResult_Failure_If_ObjectNotFound()
    {
        // Arrange
        var billId = _faker.Random.Guid();
        
        _billRepositoryMock.Setup(x => x.GetBill(billId))
            .ReturnsAsync((Bill?)null);
        
        // Act
        var result = await _service.DeleteBill(billId);
        
        // Assert
        _billRepositoryMock.Verify(x => x.GetBill(billId), Times.Once);
        _billRepositoryMock.Verify(x => x.DeleteBill(It.IsAny<Bill>()), Times.Never);
        Assert.NotNull(result);
        Assert.IsType<StandardServiceResult>(result);
        Assert.Equal(ResultType.Failure, result.Result);
        Assert.Equal("Bill not found", result.Messages.First());
    }
}