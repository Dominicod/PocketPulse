using System.ComponentModel.DataAnnotations;
using Utilities.BaseDTOs;

namespace Utilities.Test.BaseDTOs;

public class BaseDTOTest
{
    [Fact]
    public void BaseDTO_Id_ShouldHaveKeyAttribute()
    {
        // Arrange
        var propertyInfo = typeof(BaseDTO).GetProperty("Id");
        var keyAttribute = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), true);

        // Assert
        Assert.NotEmpty(keyAttribute);
    }
}