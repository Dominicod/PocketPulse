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
        var stringifiedMessages = string.Join(", ", standardServiceResult.Messages);
        _logger.LogError("Error Occured : {ErrorMessages}", stringifiedMessages);
        
        return new JsonResult(Serializer.SerializeError(standardServiceResult));
    }
}