using Utilities.Enums;
using Utilities.Shared;

namespace Utilities.Test.Shared;

public class StandardServiceResultTest
{
    [Fact]
    public void Constructor_WithResultType_SetsResult()
    {
        // Arrange
        const ResultType expectedResult = ResultType.Success;

        // Act
        var result = new StandardServiceResult(expectedResult);

        // Assert
        Assert.Equal(expectedResult, result.Result);
    }

    [Fact]
    public void Constructor_WithResultTypeAndMessage_SetsResultAndMessage()
    {
        // Arrange
        const ResultType expectedResult = ResultType.Error;
        const string expectedMessage = "An error occurred";

        // Act
        var result = new StandardServiceResult(expectedResult, expectedMessage);

        // Assert
        Assert.Equal(expectedResult, result.Result);
        Assert.Single(result.Messages);
        Assert.Equal(expectedMessage, result.Messages[0]);
    }

    [Fact]
    public void Constructor_WithResultTypeAndMessages_SetsResultAndMessages()
    {
        // Arrange
        const ResultType expectedResult = ResultType.Success;
        var expectedMessages = new List<string> { "Message 1", "Message 2" };

        // Act
        var result = new StandardServiceResult(expectedResult, expectedMessages);

        // Assert
        Assert.Equal(expectedResult, result.Result);
        Assert.Equal(expectedMessages, result.Messages);
    }
}