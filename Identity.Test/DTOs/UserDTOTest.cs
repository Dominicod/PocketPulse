using System.ComponentModel.DataAnnotations;
using Identity.API.DTOs;
using Identity.API.Models;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Identity.Test.DTOs;

public class UserDTOTest
{
    private readonly Faker _faker = new();

    # region UserDTO
    [Fact]
    public void Constructor_WithUser_SetsPropertiesCorrectly()
    {
        // Arrange
        var user = new User()
        {
            Id = _faker.Random.Guid(),
            UserName = _faker.Lorem.Sentence(),
            Email = _faker.Internet.Email(),
            CreatedAt = _faker.Date.Past(),
            UpdatedAt = _faker.Date.Past()
        };

        // Act
        var userDTO = new UserDTO(user);

        // Assert
        Assert.Equal(user.Id, userDTO.Id);
        Assert.Equal(user.UserName, userDTO.UserName);
        Assert.Equal(user.Email, userDTO.Email);
        Assert.Equal(user.CreatedAt, userDTO.CreatedAt);
        Assert.Equal(user.UpdatedAt, userDTO.UpdatedAt);
    }
    
    [Fact]
    public void UserDTO_ShouldHaveRequiredOrEmptyGuidAttributes()
    {
        // Arrange
        var userDTO = new UserDTO();

        // Act
        var validationContext = new ValidationContext(userDTO);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(userDTO, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Equal(2, validationResults.Count);
        Assert.Contains(validationResults, x => x.ErrorMessage == "The UserName field is required.");
        Assert.Contains(validationResults, x => x.ErrorMessage == "The Email field is required.");
    }
    
    [Fact]
    public void UserDTO_ShouldHaveStringLengthAttributes()
    {
        // Arrange
        var userDTO = new UserDTO
        {
            Email = "test@test.com" + _faker.Lorem.Sentence(101),
            UserName = _faker.Lorem.Sentence(101),
        };

        // Act
        var validationContext = new ValidationContext(userDTO);
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(userDTO, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults,x => x.ErrorMessage == "The field UserName must be a string with a maximum length of 100.");
        Assert.Contains(validationResults,x => x.ErrorMessage == "The field Email must be a string with a maximum length of 100.");
    }
    
    [Fact]
    public void UserDTO_ShouldHaveEmailAttributes()
    {
        // Arrange
        var userDTO = new UserDTO
        {
            Email = _faker.Lorem.Sentence(),
        };

        // Act
        var validationContext = new ValidationContext(userDTO);
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(userDTO, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults,x => x.ErrorMessage == "The Email field is not a valid e-mail address.");
    }
    # endregion
}