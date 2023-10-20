using System.ComponentModel.DataAnnotations;
using Utilities.BaseModels;

namespace Utilities.Test.BaseModels;

public class BaseModelTest
{
    [Fact]
    public void BaseModel_Id_ShouldHaveKeyAttribute()
    {
        // Arrange
        var propertyInfo = typeof(BaseModel).GetProperty("Id");
        var keyAttribute = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), true);

        // Assert
        Assert.NotEmpty(keyAttribute);
    }
}