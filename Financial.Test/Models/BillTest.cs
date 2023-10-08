using System.ComponentModel.DataAnnotations;
using Financial.API.DTOs;
using Financial.API.Models;
using Utilities.Enums;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Financial.Test.Models;

public class BillTest
{
    private readonly Faker _faker = new();
    
    # region Bill
    [Fact]
    public void Constructor_WithBillDTO_SetsPropertiesCorrectly()
    {
        // Arrange
        var billDTO = new BillDTO()
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
        var bill = new Bill(billDTO);

        // Assert
        Assert.Equal(billDTO.Id, bill.Id);
        Assert.Equal(billDTO.NickName, bill.NickName);
        Assert.Equal(billDTO.Cost, bill.Cost);
        Assert.Equal(billDTO.DueDate, bill.DueDate);
        Assert.Equal(billDTO.BillType, bill.BillType);
        Assert.Equal(billDTO.UserId, bill.UserId);
        Assert.Null(bill.CreatedAt);
        Assert.Null(bill.UpdatedAt);
    }
    
    [Fact]
    public void Bill_ShouldHaveRequiredOrEmptyGuidAttributes()
    {
        // Arrange
        var bill = new Bill();

        // Act
        var validationContext = new ValidationContext(bill);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(bill, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains(validationResults, x => x.ErrorMessage == "The Field DueDate cannot be the minimum or maximum date value.");
    }
    
    [Fact]
    public void Bill_ShouldHaveStringLengthAttributes()
    {
        // Arrange
        var bill = new Bill
        {
            NickName = _faker.Lorem.Sentence(101),
        };

        // Act
        var validationContext = new ValidationContext(bill);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(bill, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults,x => x.ErrorMessage == "The field NickName must be a string with a maximum length of 100.");
    }
    
    [Fact]
    public void Bill_ShouldHaveUTCDateAttributes()
    {
        // Arrange
        var bill = new Bill
        { 
            DueDate = DateTime.Now,
        };

        // Act
        var validationContext = new ValidationContext(bill);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(bill, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults,x => x.ErrorMessage == "The Field DueDate must be a valid UTC date.");
    }
    # endregion
}