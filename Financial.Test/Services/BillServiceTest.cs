using Financial.API.Services;
using Financial.Test.Factories;

namespace Financial.Test.Services;

public class BillServiceTest
{
    private readonly Faker _faker = new();
    private readonly BillService _service = new();

    //? Happy Path
    //?
    [Fact]
    public async Task GetAllBillsForUser_ShouldReturnString()
    {
        // Arrange
        var userId = _faker.Random.Guid();
        
        // Act
        var result = await _service.GetAllBillsForUser(userId);
        
        // Assert
        Assert.IsType<string>(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task CreateBills_ShouldReturnString()
    {
        // Arrange
        var bills = BillFactory.BuildBillDTO()
            .Generate(5);
        
        // Act
        var result = await _service.CreateBills(bills);
        
        // Assert
        Assert.IsType<string>(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task UpdateBills_ShouldReturnString()
    {
        // Arrange
        var bills = BillFactory.BuildBillDTO()
            .Generate(5);
        
        // Act
        var result = await _service.UpdateBills(bills);
        
        // Assert
        Assert.IsType<string>(result);
    }
    
    //? Happy Path
    //?
    [Fact]
    public async Task DeleteBills_ShouldReturnString()
    {
        // Arrange
        var billIds = _faker.Make(5, () => _faker.Random.Guid())
            .ToList();
        
        // Act
        var result = await _service.DeleteBills(billIds);
        
        // Assert
        Assert.IsType<string>(result);
    }
}