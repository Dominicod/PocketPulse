using System.ComponentModel.DataAnnotations;
using Identity.API.DTOs;
using Identity.API.Models;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Identity.Test.Models;

public class UserTest
{
    private readonly Faker _faker = new();
    
    # region User
    [Fact]
    public void Constructor_WithUserDTO_SetsPropertiesCorrectly()
    {
        // Arrange
        var userDTO = new UserDTO()
        {
            Id = _faker.Random.Guid(),
            UserName = _faker.Lorem.Sentence(),
            Email = _faker.Internet.Email(),
            CreatedAt = _faker.Date.Past(),
            UpdatedAt = _faker.Date.Past()
        };

        // Act
        var user = new User(userDTO);

        // Assert
        Assert.Equal(userDTO.Id, user.Id);
        Assert.Equal(userDTO.UserName, user.UserName);
        Assert.Equal(userDTO.Email, user.Email);
        Assert.Null(user.CreatedAt);
        Assert.Null(user.UpdatedAt);
    }
    
    [Fact]
    public void User_ShouldHaveRequiredOrEmptyGuidAttributes()
    {
        // Arrange
        var user = new User();

        // Act
        var validationContext = new ValidationContext(user);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Equal(2, validationResults.Count);
        Assert.Contains(validationResults, x => x.ErrorMessage == "The UserName field is required.");
        Assert.Contains(validationResults, x => x.ErrorMessage == "The Email field is required.");
    }
    
    [Fact]
    public void User_ShouldHaveStringLengthAttributes()
    {
        // Arrange
        var user = new User
        {
            Email = "test@test.com" + _faker.Lorem.Sentence(101),
            UserName = _faker.Lorem.Sentence(101),
        };

        // Act
        var validationContext = new ValidationContext(user);
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults,x => x.ErrorMessage == "The field UserName must be a string with a maximum length of 100.");
        Assert.Contains(validationResults,x => x.ErrorMessage == "The field Email must be a string with a maximum length of 100.");
    }
    
    [Fact]
    public void User_ShouldHaveEmailAttributes()
    {
        // Arrange
        var user = new User
        {
            Email = _faker.Lorem.Sentence(),
        };

        // Act
        var validationContext = new ValidationContext(user);
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults,x => x.ErrorMessage == "The Email field is not a valid e-mail address.");
    }
    # endregion
}