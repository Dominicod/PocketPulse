using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utilities.Enums;
using Utilities.Serializers;
using Utilities.Shared;

namespace Utilities.Services;

public class ResponseHandlerService : IResponseHandlerService
{
    private readonly ILogger<ResponseHandlerService> _logger;
    
    public ResponseHandlerService(ILogger<ResponseHandlerService> logger) => _logger = logger;
    
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
    
    public JsonResult GetErrorResponse(Exception exception)
    {
        _logger.LogError("Error: {Error}", exception.Message);
        
        var response = new StandardServiceResult(ResultType.Error, exception.Message);

        var contentResult = new JsonResult(Serializer.SerializeError(response))
        {
            ContentType = "application/json",
            StatusCode = StatusCodes.Status500InternalServerError
        };
        
        return contentResult;
    }
    
    public JsonResult GetOkResponse(object data)
    {
        var contentResult = new JsonResult(Serializer.SerializeData(data))
        {
            ContentType = "application/json",
            StatusCode = StatusCodes.Status200OK
        };
        
        return contentResult;
    }
    
    public JsonResult GetCreatedResponse(string uri)
    {
        var contentResult = new JsonResult(new CreatedResult(uri, null))
        {
            ContentType = "application/json",
            StatusCode = StatusCodes.Status201Created
        };
        
        return contentResult;
    }
}