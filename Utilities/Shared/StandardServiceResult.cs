using Utilities.Enums;

namespace Utilities.Shared;

public class StandardServiceResult : IStandardServiceResult
{
    public ResultType Result { get; set; }
    public List<string> Messages { get; set; } = new();
    
    public StandardServiceResult(ResultType result)
    {
        Result = result;
    }
    
    public StandardServiceResult(ResultType result, string message)
    {
        Result = result;
        Messages.Add(message);
    }
    
    public StandardServiceResult(ResultType result, List<string> messages)
    {
        Result = result;
        Messages = messages;
    }
}