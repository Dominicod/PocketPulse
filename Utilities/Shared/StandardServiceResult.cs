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
    
    public int ParseResultToStatusCode() => Result switch
    {
        ResultType.Success => 200,
        ResultType.NotFound => 404,
        ResultType.BadRequest => 400,
        ResultType.Unauthorized => 401,
        ResultType.Error => 500,
        _ => 500
    };
    
    public string ParseResultToTitle() => Result switch
    {
        ResultType.Success => "Success",
        ResultType.NotFound => "Not Found",
        ResultType.BadRequest => "Bad Request",
        ResultType.Unauthorized => "Unauthorized",
        ResultType.Error => "Error",
        _ => "Error"
    };
}