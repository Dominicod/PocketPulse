using System.ComponentModel.DataAnnotations;
using Financial.API.DTOs;
using Financial.API.Models;
using Utilities.Enums;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Financial.Test.DTOs;

public class BillDTOTest
{
    private readonly Faker _faker = new();
    
    # region BillDTO
    [Fact]
    public void Constructor_WithBill_SetsPropertiesCorrectly()
    {
        // Arrange
        var bill = new Bill()
        {
            Id = _faker.Random.Guid(),
            NickName = _faker.Lorem.Sentence(),
            Cost = _faker.Random.Decimal(),
            DueDate = _faker.Date.Future(),
            BillType = _faker.Random.Enum<BillType>(),
            UserId = _faker.Random.Guid(),
            CreatedAt = _faker.Date.Past(),
            UpdatedAt = _faker.Date.Past()
        };

        // Act
        var billDto = new BillDTO(bill);

        // Assert
        Assert.Equal(bill.Id, billDto.Id);
        Assert.Equal(bill.NickName, billDto.NickName);
        Assert.Equal(bill.Cost, billDto.Cost);
        Assert.Equal(bill.DueDate, billDto.DueDate);
        Assert.Equal(bill.BillType, billDto.BillType);
        Assert.Equal(bill.UserId, billDto.UserId);
        Assert.Equal(bill.CreatedAt, billDto.CreatedAt);
        Assert.Equal(bill.UpdatedAt, billDto.UpdatedAt);
    }
    
    [Fact]
    public void BillDTO_ShouldHaveRequiredOrEmptyGuidAttributes()
    {
        // Arrange
        var billDTO = new BillDTO();

        // Act
        var validationContext = new ValidationContext(billDTO);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(billDTO, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains(validationResults, x => x.ErrorMessage == "The Field DueDate cannot be the minimum or maximum date value.");
    }
    
    [Fact]
    public void BillDTO_ShouldHaveStringLengthAttributes()
    {
        // Arrange
        var billDTO = new BillDTO
        {
            NickName = _faker.Lorem.Sentence(101),
        };

        // Act
        var validationContext = new ValidationContext(billDTO);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(billDTO, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults,x => x.ErrorMessage == "The field NickName must be a string with a maximum length of 100.");
    }
    
    [Fact]
    public void BillDTO_ShouldHaveUTCDateAttributes()
    {
        // Arrange
        var billDTO = new BillDTO
        { 
            DueDate = DateTime.Now,
        };

        // Act
        var validationContext = new ValidationContext(billDTO);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(billDTO, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults,x => x.ErrorMessage == "The Field DueDate must be a valid UTC date.");
    }
    # endregion
}