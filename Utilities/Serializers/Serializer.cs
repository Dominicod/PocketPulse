using Utilities.Shared;

namespace Utilities.Serializers;

public static class Serializer
{
    public static object SerializeError(StandardServiceResult result)
    {
        var serializedError = new 
        {
            Error = new {
                Status = result.ParseResultToStatusCode().ToString(),
                Title = result.ParseResultToTitle(),
                Detail = result.Messages
            }
        };
        
        return serializedError;
    }
    
    public static object SerializeData(object data)
    {
        var serializedData = new 
        {
            Data = data,
        };
        
        return serializedData;
    }
}