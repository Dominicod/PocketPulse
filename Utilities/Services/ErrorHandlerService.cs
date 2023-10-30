using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utilities.Serializers;
using Utilities.Shared;

namespace Utilities.Services;

public class ErrorHandlerService : IErrorHandlerService
{
    private readonly ILogger<ErrorHandlerService> _logger;
    
    public ErrorHandlerService(ILogger<ErrorHandlerService> logger) => _logger = logger;
    
    public JsonResult GetErrorResponse(StandardServiceResult standardServiceResult)
    {
        _logger.LogError("Error: {Error}", standardServiceResult.Messages);

        var contentResult = new JsonResult(Serializer.SerializeError(standardServiceResult))
        {
            ContentType = "application/json",
            StatusCode = standardServiceResult.ParseResultToStatusCode()
        };
        
        return contentResult;
    }
}